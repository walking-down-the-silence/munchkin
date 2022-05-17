using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Expansions;
using Munchkin.Extensions.Threading;
using Munchkin.Runtime.Abstractions;
using Orleans;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Grains
{
    public class TableGrain : Grain, ITable
    {
        private readonly IPersistentState<Table> _tablePersistance;
        private readonly IMediator _mediator;
        private readonly IServiceProvider _expansionProvider;

        public TableGrain(
            [PersistentState("table", "tableStore")] IPersistentState<Table> tablePersistance,
            IMediator mediator,
            IServiceProvider expansionProvider)
        {
            _tablePersistance = tablePersistance ?? throw new ArgumentNullException(nameof(tablePersistance));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _expansionProvider = expansionProvider ?? throw new ArgumentNullException(nameof(expansionProvider));
        }

        public override async Task OnActivateAsync()
        {
            // NOTE: set available expension options to choose from
            var availableExpansions = _expansionProvider
                .GetServices<IExpansion>()
                .Select(x => new ExpansionOption(x.Code, x.Title))
                .ToList();

            _tablePersistance.State = Table.Empty()
                .WithExpansions(availableExpansions);

            await _tablePersistance.WriteStateAsync();
            await base.OnActivateAsync();
        }

        public async Task<ITable> SetupAsync()
        {
            var availableExpansions = _expansionProvider
                .GetServices<IExpansion>()
                .ToList();

            // NOTE: Set required level to win
            _tablePersistance.State = _tablePersistance.State
                .WithRequestSink(_mediator)
                .WithWinningLevel(10);

            // NOTE: Shuffle in all the selected expansions
            _tablePersistance.State = availableExpansions
                .Aggregate(_tablePersistance.State, (table, expansion) => table
                    .WithTreasureDeck(expansion.TreasureDeck.GetTreasureCards())
                    .WithDoorDeck(expansion.DoorDeck.GetDoorsCards()));

            await _tablePersistance.WriteStateAsync();
            return this.AsReference<ITable>();
        }

        public Task<IReadOnlyCollection<Player>> GetPlayersAsync() =>
            _tablePersistance.State.Players.ToList().Unit<IReadOnlyCollection<Player>>();

        public Task<Player> GetPlayerByIdAsync(string nickname) =>
            _tablePersistance.State.Players.SingleOrDefault(x => x.Nickname == nickname).Unit();

        public Task<IReadOnlyCollection<ExpansionOption>> GetAvailableExpansionsAsync() =>
            _tablePersistance.State.AvailableExpansions.Unit();

        public Task<IReadOnlyCollection<ExpansionOption>> GetIncludedExpansionsAsync() =>
            _tablePersistance.State.IncludedExpansions.Unit();

        public Task<JoinTableResult> JoinAsync(string nickname) =>
            this.Unit()
                .SelectMany(table => GrainFactory.GetGrain<IPlayer>(nickname).Unit())
                .SelectMany(player => player.GetStateAsync())
                .SelectMany(player => _tablePersistance.State.Join(player).Unit());

        public Task<JoinTableResult> LeaveAsync(string nickname) =>
            this.Unit()
                .SelectMany(table => GrainFactory.GetGrain<IPlayer>(nickname).Unit())
                .SelectMany(player => player.GetStateAsync())
                .SelectMany(player => _tablePersistance.State.Leave(player).Unit());

        public Task<SelectExpansionResult> MarkExpansionSelectionAsync(string expansionCode, bool selected) =>
            selected
                ? _tablePersistance.State.IncludeExpansion(expansionCode).Unit()
                : _tablePersistance.State.ExcludeExpansion(expansionCode).Unit();

        private static string GenerateUniqueId() => $"table_{Guid.NewGuid()}";
    }
}

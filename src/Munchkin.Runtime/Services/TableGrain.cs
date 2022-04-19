using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Munchkin.Core.Contracts;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Expansions;
using Munchkin.Core.Model.Stages;
using Munchkin.Extensions.Threading;
using Munchkin.Runtime.Abstractions.Tables;
using Orleans;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Services
{
    public class TableGrain : Grain, ITableGrain
    {
        private readonly IPersistentState<Table> _persistance;
        private readonly IMediator _mediator;
        private readonly IServiceProvider _expansionProvider;

        public TableGrain(
            [PersistentState("table", "tableStore")] IPersistentState<Table> persistance,
            IMediator mediator,
            IServiceProvider expansionProvider)
        {
            _persistance = persistance ?? throw new ArgumentNullException(nameof(persistance));
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

            _persistance.State = _persistance.State
                .WithExpansions(availableExpansions);

            await base.OnActivateAsync();
        }

        public Task<ITableGrain> SetupAsync()
        {
            var availableExpansions = _expansionProvider
                .GetServices<IExpansion>()
                .ToList();

            // NOTE: set required level to win
            _persistance.State = _persistance.State
                .WithRequestSink(_mediator)
                .WithWinningLevel(10);

            // NOTE: shuffle in all the selected expansions
            _persistance.State = availableExpansions
                .Aggregate(_persistance.State, (table, expansion) => table
                    .WithTreasureDeck(expansion.TreasureDeck.GetTreasureCards())
                    .WithDoorDeck(expansion.DoorDeck.GetDoorsCards()));

            // NOTE: shuffle the decks for randomness
            _persistance.State.DoorsCardDeck.Shuffle();
            _persistance.State.TreasureCardDeck.Shuffle();

            // NOTE: give all players initial cards
            _persistance.State.Players.ForEach(player => PlayerAvatar.Revive(_persistance.State, player));

            return this.AsReference<ITableGrain>().Unit();
        }

        public Task<IReadOnlyCollection<Player>> GetPlayersAsync() =>
            _persistance.State.Players.ToList().Unit<IReadOnlyCollection<Player>>();

        public Task<Player> GetPlayerByIdAsync(string nickname) =>
            _persistance.State.Players.SingleOrDefault(x => x.Nickname == nickname).Unit();

        public Task<JoinTableResult> JoinAsync(Player player) =>
            _persistance.State.Join(player).Unit();

        public Task<JoinTableResult> LeaveAsync(Player player) =>
            _persistance.State.Leave(player).Unit();

        public Task<IReadOnlyCollection<ExpansionSelection>> GetAvailableExpansionsAsync() =>
            _persistance.State.AvailableExpansions.Unit();

        public Task<IReadOnlyCollection<ExpansionSelection>> GetIncludedExpansionsAsync() =>
            _persistance.State.IncludedExpansions.Unit();

        public Task<SelectExpansionResult> IncludeExpansionAsync(string code) =>
            _persistance.State.IncludeExpansion(code).Unit();

        public Task<SelectExpansionResult> ExcludeExpansionAsync(string code) =>
            _persistance.State.ExcludeExpansion(code).Unit();
    }
}

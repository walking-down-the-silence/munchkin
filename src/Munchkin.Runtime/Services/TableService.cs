using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Expansions;
using Munchkin.Extensions.Threading;
using Munchkin.Runtime.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Services
{
    public class TableService
    {
        private readonly ITableRepository _tableRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IServiceProvider _expansionProvider;
        private readonly IMediator _mediator;

        public TableService(
            ITableRepository tableRepository,
            IPlayerRepository playerRepository,
            IServiceProvider expansionProvider,
            IMediator mediator)
        {
            _tableRepository = tableRepository ?? throw new ArgumentNullException(nameof(tableRepository));
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
            _expansionProvider = expansionProvider ?? throw new ArgumentNullException(nameof(expansionProvider));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public Task<Table> GetAsync(string tableId) =>
            _tableRepository.GetTableByIdAsync(tableId);

        public Task<Table> CreateAsync()
        {
            // NOTE: set available expension options to choose from
            var availableExpansions = _expansionProvider
                .GetServices<IExpansion>()
                .Select(x => new ExpansionOption(x.Code, x.Title))
                .ToList();

            var table = Table.Empty()
                .WithExpansions(availableExpansions);

            return _tableRepository.SaveTableAsync(table);
        }

        public Task<Table> SetupAsync(string tableId)
        {
            var availableExpansions = _expansionProvider
                .GetServices<IExpansion>()
                .ToList();

            // NOTE: Set required level to win
            // NOTE: Shuffle in all the selected expansions
            return ExecuteAndSave(tableId, table =>
            {
                table = table
                    .WithRequestSink(_mediator)
                    .WithWinningLevel(10);

                table = availableExpansions
                    .Aggregate(table, (table, expansion) => table
                        .WithTreasureDeck(expansion.TreasureDeck.GetTreasureCards())
                        .WithDoorDeck(expansion.DoorDeck.GetDoorsCards()));

                return table.Unit();
            });
        }

        public Task<JoinTableResult> JoinTableAsync(string tableId, string nickname)
        {
            return ExecuteAndSave(tableId, async table =>
            {
                var player = await _playerRepository.GetPlayerByNicknameAsync(nickname);
                return table.Join(player);
            });
        }

        public Task<JoinTableResult> LeaveTableAsync(string tableId, string nickname)
        {
            return ExecuteAndSave(tableId, async table =>
            {
                var player = await _playerRepository.GetPlayerByNicknameAsync(nickname);
                return table.Leave(player);
            });
        }

        public Task<SelectExpansionResult> MarkExpansionSelectionAsync(string tableId, string expansionCode, bool selected)
        {
            return ExecuteAndSave(tableId, table => selected
                    ? table.IncludeExpansion(expansionCode).Unit()
                    : table.ExcludeExpansion(expansionCode).Unit());
        }

        private async Task<TResult> ExecuteAndSave<TResult>(string tableId, Func<Table, Task<TResult>> action)
        {
            var table = await _tableRepository.GetTableByIdAsync(tableId);
            var result = await action(table);
            await _tableRepository.SaveTableAsync(table);
            return result;
        }

        private static string GenerateUniqueId() => $"table_{Guid.NewGuid()}";
    }
}

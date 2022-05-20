using MediatR;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Expansions;
using Munchkin.Core.Model.Phases;
using Munchkin.Extensions.Threading;
using Munchkin.Primitives.Abstractions;
using Munchkin.Runtime.Abstractions;
using Munchkin.Runtime.Actions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Services
{
    public class TableService
    {
        private readonly ITableRepository _tableRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IExpansionsProvider _expansionsProvider;
        private readonly IMediator _mediator;

        public TableService(
            ITableRepository tableRepository,
            IPlayerRepository playerRepository,
            IExpansionsProvider expansionsProvider,
            IMediator mediator)
        {
            _tableRepository = tableRepository ?? throw new ArgumentNullException(nameof(tableRepository));
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
            _expansionsProvider = expansionsProvider;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public static string GetUniqueId(Table table) => $"table_{table.GetHashCode()}";

        public Task<Table> GetAsync(string tableId)
        {
            return _tableRepository.GetTableByIdAsync(tableId);
        }

        public async Task<Table> CreateAsync(IShuffleAlgorithm<Card> shuffleAlgorithm = default)
        {
            // NOTE: set available expension options to choose from
            var availableExpansions = await _expansionsProvider.GetExpansionOptionsAsync();
            var table = Table.Empty(shuffleAlgorithm).WithExpansions(availableExpansions);

            return await _tableRepository.SaveTableAsync(table);
        }

        public Task<Table> SetupAsync(string tableId)
        {
            return ExecuteAndSave(tableId, async table =>
            {
                // NOTE: Set required level to win
                // NOTE: Shuffle in all the selected expansions
                var availableExpansions = await _expansionsProvider.GetExpansionsAsync();

                table = table
                    .WithRequestSink(_mediator)
                    .WithWinningLevel(10);

                table = availableExpansions
                    .Aggregate(table, (table, expansion) => table
                        .WithTreasureDeck(expansion.TreasureDeck.GetTreasureCards())
                        .WithDoorDeck(expansion.DoorDeck.GetDoorsCards()));

                var tableupdated = table.Setup();
                return (tableupdated, tableupdated);
            })
            .SelectMany(x => x.Table.Unit());
        }

        public Task<Table> DiscardAsync(string tableId, string cardId)
        {
            return ExecuteAndSave(tableId, async table =>
            {
                var doorCard = await _tableRepository.GetCardByIdAsync(tableId, cardId);
                var tableUpdated = table.Discard(doorCard);
                return (tableUpdated, tableUpdated);
            })
            .SelectMany(x => x.Table.Unit());
        }

        public Task<Table> PlayAsync(string tableId, string playerNickname, string cardId)
        {
            return ExecuteAndSave(tableId, async table =>
            {
                // TODO: move this to PlayerService
                var card = await _tableRepository.GetCardByIdAsync(tableId, cardId);
                table = table.Play(card);
                return (table, table);
            })
            .SelectMany(x => x.Table.Unit());
        }

        public Task<Table> CursePlayerAsync(string tableId, string nickname, string curseCardId)
        {
            return ExecuteAndSave(tableId, async table =>
            {
                var player = await _playerRepository.GetPlayerByNicknameAsync(nickname);
                var card = await _tableRepository.GetCardByIdAsync(tableId, curseCardId) as CurseCard;
                var tableUpdated = Dungeon.Curse(table, card, player);
                return (tableUpdated, tableUpdated);
            })
            .SelectMany(x => x.Table.Unit());
        }

        public Task<Table> EquipAsync(string tableId, string playerNickname, string cardId)
        {
            return ExecuteAndSave(tableId, async table =>
            {
                // TODO: move this to PlayerService
                var player = await _playerRepository.GetPlayerByNicknameAsync(playerNickname);
                var card = await _tableRepository.GetCardByIdAsync(tableId, cardId);
                player.Equip(card);
                return (table, table);
            })
            .SelectMany(x => x.Table.Unit());
        }

        public async Task<Table> NextAsync(string tableId)
        {
            var table = await _tableRepository.GetTableByIdAsync(tableId);
            table = await _tableRepository.SaveTableAsync(table.NextTurn());
            await _mediator.Publish(new SetPlayerTurnActions(null));
            return table;
        }

        public Task<JoinTableResult> JoinTableAsync(string tableId, string nickname)
        {
            return ExecuteAndSave(tableId, async table =>
            {
                var player = await _playerRepository.GetPlayerByNicknameAsync(nickname);
                var (tableUpdated, result) = table.Join(player);
                return (tableUpdated, result);
            })
            .SelectMany(x => x.Result.Unit());
        }

        public Task<JoinTableResult> LeaveTableAsync(string tableId, string nickname)
        {
            return ExecuteAndSave(tableId, async table =>
            {
                var player = await _playerRepository.GetPlayerByNicknameAsync(nickname);
                var (tableUpdated, result) = table.Leave(player);
                return (tableUpdated, result);
            })
            .SelectMany(x => x.Result.Unit());
        }

        public Task<SelectExpansionResult> MarkExpansionSelectionAsync(string tableId, string expansionCode, bool selected)
        {
            return ExecuteAndSave(tableId, table =>
            {
                var result = selected
                    ? table.IncludeExpansion(expansionCode).Result
                    : table.ExcludeExpansion(expansionCode).Result;
                return (table, result).Unit();
            })
            .SelectMany(x => x.Result.Unit());
        }

        private async Task<(Table Table, TResult Result)> ExecuteAndSave<TResult>(
            string tableId,
            Func<Table, Task<(Table Table, TResult Result)>> action)
        {
            var table = await _tableRepository.GetTableByIdAsync(tableId);
            var result = await action(table);
            var tableUpdated = await _tableRepository.SaveTableAsync(result.Table);
            return (tableUpdated, result.Result);
        }
    }
}

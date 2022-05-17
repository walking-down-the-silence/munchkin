using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Phases;
using Munchkin.Extensions.Threading;
using Munchkin.Runtime.Abstractions;
using System;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Services
{
    public class DungeonService
    {
        private readonly ITableRepository _tableRepository;
        private readonly IPlayerRepository _playerRepository;

        public DungeonService(
            ITableRepository tableRepository,
            IPlayerRepository playerRepository)
        {
            _tableRepository = tableRepository ?? throw new ArgumentNullException(nameof(tableRepository));
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }

        public Task PlayCardAsync(string tableId, string nickname, string cardId)
        {
            return ExecuteAndSave(tableId, async table =>
            {
                var player = await _playerRepository.GetPlayerByNicknameAsync(nickname);
                var card = await _tableRepository.GetCardByIdAsync(tableId, cardId);
                table = table.Play(card);
                return (table, table);
            });
        }

        public Task KickOpenTheDoorAsync(string tableId)
        {
            // TODO: check if kick open the door can be executed
            // 1. if the player had not yet kick opened the door
            return ExecuteAndSave(tableId, table =>
            {
                var tableUpdated = Dungeon.KickOpenTheDoor(table);
                return (tableUpdated, tableUpdated).Unit();
            });
        }

        public Task LootTheRoomAsync(string tableId)
        {
            // TODO: check if loot the room can be executed
            // 1. if the kick open the door did not result in combat
            // 2. if no curses prevent player from looting the room
            return ExecuteAndSave(tableId, table =>
            {
                var tableUpdated = Dungeon.LootTheRoom(table);
                return (tableUpdated, tableUpdated).Unit();
            });
        }

        public Task LookForTroubleAsync(string tableId, string monsterCardId)
        {
            // TODO: check if look for trouble can be executed
            // 1. if the combat had not yet took place (kick open the door)
            // 2. if the combat had not yet took place (look for trouble)
            return ExecuteAndSave(tableId, async table =>
            {
                var monster = await _tableRepository.GetCardByIdAsync(tableId, monsterCardId) as MonsterCard;
                var tableUpdated = Dungeon.LookForTrouble(table, monster);
                return (tableUpdated, tableUpdated);
            });
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

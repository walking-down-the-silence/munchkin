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

        public DungeonService(
            ITableRepository tableRepository)
        {
            _tableRepository = tableRepository ?? throw new ArgumentNullException(nameof(tableRepository));
        }

        public Task<Table> KickOpenTheDoorAsync(string tableId)
        {
            // TODO: check if kick open the door can be executed
            // 1. if the player had not yet kick opened the door
            return ExecuteAndSave(tableId, table =>
            {
                var tableUpdated = Dungeon.KickOpenTheDoor(table);
                return (tableUpdated, tableUpdated).Unit();
            })
            .SelectMany(x => x.Table.Unit());
        }

        public Task<Table> LootTheRoomAsync(string tableId)
        {
            // TODO: check if loot the room can be executed
            // 1. if the kick open the door did not result in combat
            // 2. if no curses prevent player from looting the room
            return ExecuteAndSave(tableId, table =>
            {
                var tableUpdated = Dungeon.LootTheRoom(table);
                return (tableUpdated, tableUpdated).Unit();
            })
            .SelectMany(x => x.Table.Unit());
        }

        public Task<Table> LookForTroubleAsync(string tableId, string monsterCardId)
        {
            // TODO: check if look for trouble can be executed
            // 1. if the combat had not yet took place (kick open the door)
            // 2. if the combat had not yet took place (look for trouble)
            return ExecuteAndSave(tableId, async table =>
            {
                var monster = await _tableRepository.GetCardByIdAsync(tableId, monsterCardId) as MonsterCard;
                var tableUpdated = Dungeon.LookForTrouble(table, monster);
                return (tableUpdated, tableUpdated);
            })
            .SelectMany(x => x.Table.Unit());
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

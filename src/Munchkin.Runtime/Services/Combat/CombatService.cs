using Munchkin.Core.Model;
using Munchkin.Core.Model.Phases;
using Munchkin.Extensions.Threading;
using Munchkin.Runtime.Abstractions;
using System;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Services
{
    public class CombatService
    {
        private readonly ITableRepository _tableRepository;

        public CombatService(
            ITableRepository tableRepository)
        {
            _tableRepository = tableRepository ?? throw new ArgumentNullException(nameof(tableRepository));
        }

        public Task<Table> RewardAsync(string tableId)
        {
            return ExecuteAndSave(tableId, table =>
            {
                var tableUpdated = Combat.Reward(table);
                return (tableUpdated, tableUpdated).Unit();
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

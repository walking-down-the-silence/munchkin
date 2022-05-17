using Munchkin.Core.Model;
using Munchkin.Runtime.Abstractions;
using System;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Services
{
    public class RunningAwayService
    {
        private readonly ITableRepository _tableRepository;

        public RunningAwayService(
            ITableRepository tableRepository)
        {
            _tableRepository = tableRepository ?? throw new ArgumentNullException(nameof(tableRepository));
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

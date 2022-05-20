using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Extensions.Threading;
using Munchkin.Runtime.Abstractions;
using Munchkin.Runtime.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Repositories
{
    public class InMemoryTableRepository : ITableRepository
    {
        private readonly Dictionary<string, Table> _tables = new();

        public Task<Table> GetTableByIdAsync(string tableId)
        {
            return _tables.ContainsKey(tableId)
                ? Task.FromResult(_tables[tableId])
                : Task.FromResult<Table>(null);
        }

        public Task<Table> SaveTableAsync(Table table)
        {
            _ = table is not null
                ? (_tables[TableService.GetUniqueId(table)] = table)
                : null;
            return Task.FromResult(table);
        }

        public Task<Card> GetCardByIdAsync(string tableId, string cardId)
        {
            return GetTableByIdAsync(tableId).SelectMany(table =>
            {
                return table.FindCard(card => card.Code == cardId).Unit();
            });
        }
    }
}

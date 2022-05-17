using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Abstractions
{
    public interface ITableRepository
    {
        Task<Table> GetTableByIdAsync(string tableId);

        Task<Table> SaveTableAsync(Table table);

        Task<Card> GetCardByIdAsync(string tableId, string cardId);
    }
}

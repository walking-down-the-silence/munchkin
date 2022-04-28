using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Abstractions
{
    public interface ITableRepository
    {
        Task<Table> GetTableByIdAsync(string tableId);

        Task<Table> SaveTableAsync(Table table);
    }
}

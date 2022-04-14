using System.Threading.Tasks;

namespace Munchkin.Runtime.Abstractions.Tables
{
    public interface ITableRepository
    {
        Task<ITable> GetTableByIdAsync(string tableId);

        Task<ITable> SaveTableAsync(ITable table);

        Task<bool> DropTableAsync(string tableId);
    }
}

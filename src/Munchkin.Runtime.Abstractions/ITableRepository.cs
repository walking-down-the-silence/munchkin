using System.Threading.Tasks;

namespace Munchkin.Runtime.Abstractions
{
    public interface ITableRepository
    {
        Task<ITable> GetTableByIdAsync(string tableId);

        Task<ITable> SaveTableAsync(ITable table);

        Task<bool> DeleteTableAsync(string tableId);
    }
}

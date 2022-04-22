using System.Threading.Tasks;

namespace Munchkin.Runtime.Abstractions
{
    public interface ITableRepository
    {
        Task<ITableGrain> GetTableByIdAsync(string tableId);

        Task<ITableGrain> SaveTableAsync(ITableGrain table);

        Task<bool> DeleteTableAsync(string tableId);
    }
}

using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Abstractions
{
    public interface ITurnRepository
    {
        Task<Turn> GetTurnByTableIdAsync(string tableId);

        Task<Turn> SaveTurnAsync(Turn turn);
    }
}

using System.Threading.Tasks;

namespace Munchkin.Runtime.Abstractions.GameEngines
{
    public interface IGameEngineRepository
    {
        Task<IGameEngine> GetGameByIdAsync(string tableId);

        Task<IGameEngine> SaveGameAsync(IGameEngine game);
    }
}

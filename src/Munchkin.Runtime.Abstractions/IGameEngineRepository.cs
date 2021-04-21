using System.Threading.Tasks;

namespace Munchkin.Runtime.Abstractions
{
    public interface IGameEngineRepository
    {
        Task<IGameEngine> GetGameByIdAsync(int gameId);

        Task<IGameEngine> SaveGameAsync(IGameEngine game);
    }
}

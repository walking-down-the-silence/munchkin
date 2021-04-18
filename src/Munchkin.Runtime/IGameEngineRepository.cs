using System.Threading.Tasks;

namespace Munchkin.Runtime
{
    public interface IGameEngineRepository
    {
        Task<GameEngine> GetGameByIdAsync(int gameId);

        Task<GameEngine> SaveGameAsync(GameEngine game);
    }
}

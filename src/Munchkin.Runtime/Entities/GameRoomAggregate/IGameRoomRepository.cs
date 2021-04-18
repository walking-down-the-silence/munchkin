using System.Threading.Tasks;

namespace Munchkin.Runtime.Entities.GameRoomAggregate
{
    public interface IGameRoomRepository
    {
        Task<GameRoom> GetGameRoomByIdAsync(int gameRoomId);

        Task<GameRoom> SaveGameRoomAsync(GameRoom gameRoom);

        Task<bool> DropGameRoomAsync(int gameRoomId);
    }
}

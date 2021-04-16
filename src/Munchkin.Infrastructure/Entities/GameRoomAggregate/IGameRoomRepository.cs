using System.Threading.Tasks;

namespace Munchkin.Infrastructure.Models
{
    public interface IGameRoomRepository
    {
        Task<GameRoom> GetGameRoomById(int gameRoomId);

        Task<GameRoom> SaveGameRoom(GameRoom gameRoom);

        Task<bool> DropGameRoom(int gameRoomId);
    }
}

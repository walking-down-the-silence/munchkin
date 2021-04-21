using System.Threading.Tasks;

namespace Munchkin.Runtime.Abstractions.GameRoomAggregate
{
    public interface IGameRoomRepository
    {
        Task<IGameRoom> GetGameRoomByIdAsync(int gameRoomId);

        Task<IGameRoom> SaveGameRoomAsync(IGameRoom gameRoom);

        Task<bool> DropGameRoomAsync(int gameRoomId);
    }
}

using Munchkin.Runtime.Abstractions.GameRoomAggregate;
using System;
using System.Threading.Tasks;

namespace Munchkin.Services.Lobby.Repositories
{
    public class GameRoomRepository : IGameRoomRepository
    {
        public Task<bool> DropGameRoomAsync(int gameRoomId)
        {
            throw new NotImplementedException();
        }

        public Task<IGameRoom> GetGameRoomByIdAsync(int gameRoomId)
        {
            throw new NotImplementedException();
        }

        public Task<IGameRoom> SaveGameRoomAsync(IGameRoom gameRoom)
        {
            throw new NotImplementedException();
        }
    }
}

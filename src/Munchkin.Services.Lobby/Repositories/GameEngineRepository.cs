using Munchkin.Runtime.Abstractions;
using System;
using System.Threading.Tasks;

namespace Munchkin.Services.Lobby.Repositories
{
    public class GameEngineRepository : IGameEngineRepository
    {
        public Task<IGameEngine> GetGameByIdAsync(int gameId)
        {
            throw new NotImplementedException();
        }

        public Task<IGameEngine> SaveGameAsync(IGameEngine game)
        {
            throw new NotImplementedException();
        }
    }
}

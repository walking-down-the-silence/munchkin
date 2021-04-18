using Munchkin.Runtime.Entities.GameRoomAggregate;
using Munchkin.Runtime.Entities.UserAggregate;
using System;
using System.Threading.Tasks;

namespace Munchkin.Infrastructure.Services
{
    public class GameRoomService
    {
        private readonly IGameRoomRepository _gameRoomRepository;

        public GameRoomService(IGameRoomRepository gameRoomRepository)
        {
            _gameRoomRepository = gameRoomRepository ?? throw new ArgumentNullException(nameof(gameRoomRepository));
        }

        public async Task<GameRoom> CreateRoomAsLeader(User user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            var gameRoom = new GameRoom();
            var (result, joinResponse) = gameRoom.JoinRoom(user);
            result = await _gameRoomRepository.SaveGameRoomAsync(result);
            return joinResponse == JoinRoomResult.JoinedRoom ? result : default;
        }

        public async Task<JoinRoomResult> JoinTheRoom(int gameRoomId, User user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            var gameRoom = await _gameRoomRepository.GetGameRoomByIdAsync(gameRoomId);
            var (result, joinResponse) = gameRoom.JoinRoom(user);

            if (joinResponse == JoinRoomResult.JoinedRoom)
            {
                _ = await _gameRoomRepository.SaveGameRoomAsync(result);
            }

            return joinResponse;
        }

        public async Task<JoinRoomResult> LeaveTheRoom(int gameRoomId, User user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            var gameRoom = await _gameRoomRepository.GetGameRoomByIdAsync(gameRoomId);
            var (result, joinResponse) = gameRoom.LeaveRoom(user);

            if (joinResponse == JoinRoomResult.LeftRoom)
            {
                result = await _gameRoomRepository.SaveGameRoomAsync(result);
            }

            if (result.IsEmpty)
            {
                _ = await _gameRoomRepository.DropGameRoomAsync(gameRoomId);
            }

            return joinResponse;
        }
    }
}

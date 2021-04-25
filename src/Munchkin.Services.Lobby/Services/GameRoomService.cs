using Munchkin.Runtime.Abstractions.GameRoomAggregate;
using Munchkin.Runtime.Abstractions.UserAggregate;
using System;
using System.Threading.Tasks;

namespace Munchkin.Services.Lobby.Services
{
    public class GameRoomService
    {
        private readonly IGameRoomRepository _gameRoomRepository;
        private readonly IUserRepository _userRepository;

        public GameRoomService(
            IGameRoomRepository gameRoomRepository,
            IUserRepository userRepository)
        {
            _gameRoomRepository = gameRoomRepository ?? throw new ArgumentNullException(nameof(gameRoomRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<IGameRoom> CreateRoomAsLeader(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user is null)
            {
                return null;
            }

            // TODO: replace the game room id with correct one
            IGameRoom gameRoom = await _gameRoomRepository.GetGameRoomByIdAsync(1);
            var joinResponse = await gameRoom.JoinRoom(user);
            gameRoom = await _gameRoomRepository.SaveGameRoomAsync(gameRoom);
            return joinResponse == JoinRoomResult.JoinedRoom ? gameRoom : default;
        }

        public async Task<JoinRoomResult> JoinTheRoom(int gameRoomId, int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user is null)
            {
                return JoinRoomResult.InvalidUser;
            }

            var gameRoom = await _gameRoomRepository.GetGameRoomByIdAsync(gameRoomId);
            var joinResponse = await gameRoom.JoinRoom(user);

            if (joinResponse == JoinRoomResult.JoinedRoom)
            {
                _ = await _gameRoomRepository.SaveGameRoomAsync(gameRoom);
            }

            return joinResponse;
        }

        public async Task<JoinRoomResult> LeaveTheRoom(int gameRoomId, int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user is null)
            {
                return JoinRoomResult.InvalidUser;
            }

            var gameRoom = await _gameRoomRepository.GetGameRoomByIdAsync(gameRoomId);
            var joinResponse = await gameRoom.LeaveRoom(user);

            if (joinResponse == JoinRoomResult.LeftRoom)
            {
                gameRoom = await _gameRoomRepository.SaveGameRoomAsync(gameRoom);
            }

            if (await gameRoom.IsEmpty())
            {
                _ = await _gameRoomRepository.DropGameRoomAsync(gameRoomId);
            }

            return joinResponse;
        }
    }
}

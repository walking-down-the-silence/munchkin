using Munchkin.Runtime.Abstractions.GameRoomAggregate;
using Munchkin.Runtime.Abstractions.UserAggregate;
using Orleans;
using System;
using System.Threading.Tasks;

namespace Munchkin.Services.Lobby.Services
{
    public class GameRoomService
    {
        private readonly IUserRepository _userRepository;
        private readonly IClusterClient _clusterClient;

        public GameRoomService(
            IUserRepository userRepository,
            IClusterClient clusterClient)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _clusterClient = clusterClient ?? throw new ArgumentNullException(nameof(clusterClient));
        }

        public async Task<IGameRoom> CreateRoomAsLeader(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user is null)
            {
                return null;
            }

            var gameRoom = _clusterClient.GetGrain<IGameRoom>(userId);
            var joinResponse = await gameRoom.JoinRoom(user);
            return joinResponse == JoinRoomResult.JoinedRoom ? gameRoom : default;
        }

        public Task<IGameRoom> GetGameRoom(int gameRoomId)
        {
            var gameRoom = _clusterClient.GetGrain<IGameRoom>(gameRoomId);
            return Task.FromResult(gameRoom);
        }

        public async Task<JoinRoomResult> JoinTheRoom(int gameRoomId, int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user is null)
            {
                return JoinRoomResult.InvalidUser;
            }

            var gameRoom = _clusterClient.GetGrain<IGameRoom>(gameRoomId);
            var joinResponse = await gameRoom.JoinRoom(user);
            return joinResponse;
        }

        public async Task<JoinRoomResult> LeaveTheRoom(int gameRoomId, int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user is null)
            {
                return JoinRoomResult.InvalidUser;
            }

            var gameRoom = _clusterClient.GetGrain<IGameRoom>(gameRoomId);
            var joinResponse = await gameRoom.LeaveRoom(user);
            return joinResponse;
        }
    }
}

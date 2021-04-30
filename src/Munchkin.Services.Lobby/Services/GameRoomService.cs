using Microsoft.Extensions.DependencyInjection;
using Munchkin.Core.Contracts;
using Munchkin.Runtime.Abstractions.GameRoomAggregate;
using Munchkin.Runtime.Abstractions.UserAggregate;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Services.Lobby.Services
{
    public class GameRoomService
    {
        private readonly IUserRepository _userRepository;
        private readonly IClusterClient _clusterClient;
        private readonly IServiceProvider _expansionProvider;

        public GameRoomService(
            IUserRepository userRepository,
            IClusterClient clusterClient,
            IServiceProvider expansionProvider)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _clusterClient = clusterClient ?? throw new ArgumentNullException(nameof(clusterClient));
            _expansionProvider = expansionProvider ?? throw new ArgumentNullException(nameof(expansionProvider));
        }

        public async Task<IGameRoom> CreateRoomAsLeader(int userId)
        {
            var availableExpansions = _expansionProvider
                .GetServices<IExpansion>()
                .Select(x => new ExpansionOption(x.Code, x.Title))
                .ToArray();

            var user = await _userRepository.GetUserByIdAsync(userId);

            var gameRoom = _clusterClient.GetGrain<IGameRoom>(userId);
            await gameRoom.SetAvailableExpansions(availableExpansions);
            
            var joinResponse = user is not null
                ? await gameRoom.JoinRoom(user)
                : JoinRoomResult.InvalidUser;

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
            var gameRoom = _clusterClient.GetGrain<IGameRoom>(gameRoomId);
            var joinResponse = user is not null
                ? await gameRoom.JoinRoom(user)
                : JoinRoomResult.InvalidUser;
            return joinResponse;
        }

        public async Task<JoinRoomResult> LeaveTheRoom(int gameRoomId, int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            var gameRoom = _clusterClient.GetGrain<IGameRoom>(gameRoomId);
            var joinResponse = user is not null
                ? await gameRoom.LeaveRoom(user)
                : JoinRoomResult.InvalidUser;
            return joinResponse;
        }

        public Task<IReadOnlyCollection<ExpansionSelection>> GetExpansionSelections(int gameRoomId)
        {
            var gameRoom = _clusterClient.GetGrain<IGameRoom>(gameRoomId);
            return gameRoom.GetExpansionSelections();
        }

        public Task<SelectExpansionResult> MarkExpansionSelection(int gameRoomId, string expansionCode, bool selected)
        {
            var gameRoom = _clusterClient.GetGrain<IGameRoom>(gameRoomId);
            var result = selected
                ? gameRoom.SelectExpansion(expansionCode)
                : gameRoom.UnselectExpansion(expansionCode);
            return result;
        }
    }
}

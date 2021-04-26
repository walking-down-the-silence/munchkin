using Munchkin.Runtime.Abstractions.GameRoomAggregate;
using Munchkin.Runtime.Abstractions.UserAggregate;
using Munchkin.Runtime.Entities.GameRoomAggregate;
using Munchkin.Services.Lobby.Services;
using System.Threading.Tasks;
using Orleans.TestingHost;
using Xunit;

namespace Munchkin.Runtime.Client.Tests.Services
{
    public class GameRoomServiceTests
    {
        [Fact]
        public async void CreateRoom_WithNotNullParameter_ShouldCreateGameRoom()
        {
            // Arrange
            using var cluster = CreateTestCluster();
            int userId = 1;
            var userRepository = new UserRepositoryDummy();
            var gameRoomService = new GameRoomService(userRepository, cluster.Client);

            // Act
            var gameRoom = await gameRoomService.CreateRoomAsLeader(userId);
            var players = await gameRoom.GetPlayers();

            // Assert
            Assert.NotNull(gameRoom);
            Assert.Single(players);
        }

        private class UserRepositoryDummy : IUserRepository
        {
            public Task<User> GetUserByIdAsync(int userId)
            {
                return Task.FromResult(new User(userId, "Johny Cash"));
            }
        }

        private class GameRoomRepositoryDummy : IGameRoomRepository
        {
            public Task<bool> DropGameRoomAsync(int gameRoomId) => Task.FromResult(true);

            public Task<IGameRoom> GetGameRoomByIdAsync(int gameRoomId) => Task.FromResult<IGameRoom>(new GameRoom());

            public Task<IGameRoom> SaveGameRoomAsync(IGameRoom gameRoom) => Task.FromResult(gameRoom);
        }

        private TestCluster CreateTestCluster()
        {
            var builder = new TestClusterBuilder();
            var cluster = builder.Build();
            cluster.Deploy();

            return cluster;
        }
    }
}

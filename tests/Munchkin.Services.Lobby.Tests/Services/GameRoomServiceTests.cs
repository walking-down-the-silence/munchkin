using Munchkin.Runtime.Abstractions.GameRoomAggregate;
using Munchkin.Runtime.Abstractions.UserAggregate;
using Munchkin.Runtime.Entities.GameRoomAggregate;
using Munchkin.Services.Lobby.Services;
using System.Threading.Tasks;
using Orleans.TestingHost;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Munchkin.Services.Lobby;

namespace Munchkin.Runtime.Client.Tests.Services
{
    public class GameRoomServiceTests
    {
        [Fact]
        public async void CreateRoom_WithNotNullParameter_ShouldCreateGameRoom()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddMunchkinGameServices();
            using var serviceProvider = serviceCollection.BuildServiceProvider();

            using var cluster = CreateTestCluster();
            int userId = 1;
            var userRepository = new UserRepositoryDummy();
            var gameRoomService = new GameRoomService(userRepository, cluster.Client, serviceProvider);

            // Act
            var gameRoom = await gameRoomService.CreateRoomAsLeader(userId);
            var players = await gameRoom.GetUsers();

            // Assert
            Assert.NotNull(gameRoom);
            Assert.Single(players);
        }

        private class UserRepositoryDummy : IUserRepository
        {
            public Task<User> GetUserByIdAsync(int userId)
            {
                return Task.FromResult(new User(userId, "Johny Cash", true));
            }

            public Task SaveUserAsync(User user)
            {
                throw new System.NotImplementedException();
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

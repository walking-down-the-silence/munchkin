using Microsoft.Extensions.DependencyInjection;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using Munchkin.Runtime.Abstractions.UserAggregate;
using Munchkin.Services.Lobby;
using Munchkin.Services.Lobby.Repositories;
using Munchkin.Services.Lobby.Services;
using Orleans.TestingHost;
using System.Threading.Tasks;
using Xunit;

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
            var tableId = "table_1";
            var tableRepository = new TableRepository(cluster.Client);
            var playerRepository = new PlayerRepositoryDummy();
            var gameRoomService = new TableService(playerRepository, tableRepository, serviceProvider);

            // Act
            var gameRoom = await gameRoomService.CreateTableAsLeader(tableId);
            var players = await gameRoom.GetPlayers();

            // Assert
            Assert.NotNull(gameRoom);
            Assert.Single(players);
        }

        private class PlayerRepositoryDummy : IPlayerRepository
        {
            public Task<Player> GetPlayerByNicknameAsync(string nickname) =>
                Task.FromResult(new Player(nickname, EGender.Male));

            public Task SavePlayerAsync(Player user) =>
                throw new System.NotImplementedException();
        }

        private static TestCluster CreateTestCluster()
        {
            var builder = new TestClusterBuilder();
            var cluster = builder.Build();
            cluster.Deploy();

            return cluster;
        }
    }
}

using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Services.Lobby.Services;
using Orleans.TestingHost;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Munchkin.Runtime.Client.Tests.Services
{
    public class TableServiceTests
    {
        [Fact]
        public async void SetupAsync_WithNotNullParameter_ShouldCreateGameRoom()
        {
            // Arrange
            using var cluster = CreateTestCluster();
            var tableId = "table_1";
            var playerRepository = new PlayerRepositoryDummy();
            var gameRoomService = new TableService(cluster.Client, playerRepository);

            // Act
            var gameRoom = await gameRoomService.SetupAsync(tableId);
            var players = await gameRoom.GetPlayersAsync();

            // Assert
            Assert.NotNull(gameRoom);
            Assert.Single(players);
        }

        private class PlayerRepositoryDummy : IPlayerRepository
        {
            public Task<Player> GetPlayerByNicknameAsync(string nickname) =>
                Task.FromResult(new Player(nickname, EGender.Male));

            public Task<IReadOnlyCollection<Player>> GetPlayersAsync(string tableId) =>
                throw new System.NotImplementedException();

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

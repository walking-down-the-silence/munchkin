using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Runtime.Abstractions;
using Munchkin.Runtime.Services;
using Orleans.TestingHost;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Munchkin.Runtime.Tests.Services
{
    public class TableServiceTests
    {
        [Fact]
        public async void SetupAsync_WithNotNullParameter_ShouldCreateGameRoom()
        {
            // Arrange
            using var cluster = CreateTestCluster();
            var tableId = "table_1";
            var tableRepository = new TableRepositoryDummy();
            var playerRepository = new PlayerRepositoryDummy();
            var tableService = new TableService(tableRepository, playerRepository, null, null);

            // Act
            var table = await tableService.SetupAsync(tableId);

            // Assert
            Assert.NotNull(table);
            Assert.Single(table.Players);
        }

        private class TableRepositoryDummy : ITableRepository
        {
            public Task<Table> GetTableByIdAsync(string tableId) =>
                throw new System.NotImplementedException();

            public Task<Table> SaveTableAsync(Table table) =>
                throw new System.NotImplementedException();
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

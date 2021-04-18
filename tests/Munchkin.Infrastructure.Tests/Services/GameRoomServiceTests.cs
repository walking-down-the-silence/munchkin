using Moq;
using Munchkin.Infrastructure.Services;
using Munchkin.Runtime.Entities.GameRoomAggregate;
using Munchkin.Runtime.Entities.UserAggregate;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Munchkin.Infrastructure.Tests.Services
{
    public class GameRoomServiceTests
    {
        [Fact]
        public void CreateRoom_WithNullParameter_ShoudThrowArgumentNullException()
        {
            // Arrange
            var gameRoomRepository = Mock.Of<IGameRoomRepository>();
            var gameRoomService = new GameRoomService(gameRoomRepository);

            // Act, Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => gameRoomService.CreateRoomAsLeader(null));
        }

        [Fact]
        public async void CreateRoom_WithNotNullParameter_ShouldCreateGameRoom()
        {
            // Arrange
            var user = new User(1, "Johny Cash");

            var gameRoomRepository = new GameRoomRepositoryDummy();
            var gameRoomService = new GameRoomService(gameRoomRepository);

            // Act
            GameRoom gameRoom = await gameRoomService.CreateRoomAsLeader(user);

            // Assert
            Assert.NotNull(gameRoom);
            Assert.Single(gameRoom.Players);
        }

        private class GameRoomRepositoryDummy : IGameRoomRepository
        {
            public Task<bool> DropGameRoomAsync(int gameRoomId) => throw new NotImplementedException();

            public Task<GameRoom> GetGameRoomByIdAsync(int gameRoomId) => throw new NotImplementedException();

            public Task<GameRoom> SaveGameRoomAsync(GameRoom gameRoom) => Task.FromResult(gameRoom);
        }
    }
}

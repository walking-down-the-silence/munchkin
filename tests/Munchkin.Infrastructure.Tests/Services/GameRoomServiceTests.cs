using Munchkin.Infrastructure.Entities.UserAggregate;
using Munchkin.Infrastructure.Models;
using Munchkin.Infrastructure.Services;
using System;
using Xunit;

namespace Munchkin.Infrastructure.Tests.Services
{
    public class GameRoomServiceTests
    {
        [Fact]
        public void CreateRoom_WithNullParameter_ShoudThrowArgumentNullException()
        {
            // Arrange
            var gameRoomService = new GameRoomService();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => gameRoomService.CreateRoom(null));
        }

        [Fact]
        public void CreateRoom_WithNotNullParameter_ShouldCreateGameRoom()
        {
            // Arrange
            var user = new User(1, "Johny Cash");
            var gameRoomService = new GameRoomService();

            // Act
            GameRoom gameRoom = gameRoomService.CreateRoom(user);

            // Assert
            Assert.NotNull(gameRoom);
            Assert.Single(gameRoom.Players);
        }
    }
}

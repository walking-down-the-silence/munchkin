using Munchkin.Infrastructure.Entities.UserAggregate;
using Munchkin.Infrastructure.Models;
using System;
using Xunit;

namespace Munchkin.Infrastructure.Tests
{
    public class GameRoomTests
    {
        [Fact]
        public void Create_WithNullParameter_ShouldThrowArgumentNullException()
        {
            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => GameRoom.Create(null));
        }

        [Fact]
        public void Create_WithNotNullParameter_ShouldCreateGameRoomWithSinglePlayer()
        {
            // Arrange
            var user = new User(1, "Johny Cash");

            // Act
            var gameRoom = GameRoom.Create(user);

            // Assert
            Assert.NotNull(gameRoom);
            Assert.Single(gameRoom.Players);
            Assert.False(gameRoom.IsEmpty);
        }

        [Fact]
        public void JoinRoom_WithNullParameter_ShouldNotJoinTheRoom()
        {
            // Arrange
            var user = new User(1, "Johny Cash");
            var expectedResult = JoinRoomResult.InvalidUser;

            // Act
            var gameRoom = GameRoom.Create(user);
            var joinResponse = gameRoom.JoinRoom(null);

            // Assert
            Assert.Equal(expectedResult, joinResponse);
            Assert.Single(gameRoom.Players);
            Assert.False(gameRoom.IsEmpty);
        }

        [Fact]
        public void JoinRoom_WithNotNullParameter_ShouldJoinAndHaveExactlyTwoPlayers()
        {
            // Arrange
            var user1 = new User(1, "Johny Cash");
            var user2 = new User(2, "Frank Sinatra");
            var expectedResponse = JoinRoomResult.JoinedRoom;
            int expectedCount = 2;

            // Act
            var gameRoom = GameRoom.Create(user1);
            var joinResponse = gameRoom.JoinRoom(user2);

            // Assert
            Assert.Equal(expectedResponse, joinResponse);
            Assert.Equal(expectedCount, gameRoom.Players.Count);
            Assert.False(gameRoom.IsEmpty);
        }

        [Fact]
        public void LeaveRoom_WithNullParameter_ShouldNotLeaveTheRoom()
        {
            // Arrange
            var user1 = new User(1, "Johny Cash");
            var expectedResponse = JoinRoomResult.InvalidUser;

            // Act
            var gameRoom = GameRoom.Create(user1);
            var joinResponse = gameRoom.LeaveRoom(null);

            // Assert
            Assert.Equal(expectedResponse, joinResponse);
            Assert.Single(gameRoom.Players);
            Assert.False(gameRoom.IsEmpty);
        }

        [Fact]
        public void LeaveRoom_WithNotNullParameter_ShouldLeaveAndGameRoomBecomeEmpty()
        {
            // Arrange
            var user1 = new User(1, "Johny Cash");
            var expectedResponse = JoinRoomResult.LeftRoom;

            // Act
            var gameRoom = GameRoom.Create(user1);
            var joinResponse = gameRoom.LeaveRoom(user1);

            // Assert
            Assert.Equal(expectedResponse, joinResponse);
            Assert.Empty(gameRoom.Players);
            Assert.True(gameRoom.IsEmpty);
        }
    }
}

using Munchkin.Runtime.Abstractions.GameRoomAggregate;
using Munchkin.Runtime.Abstractions.UserAggregate;
using Munchkin.Runtime.Entities.GameRoomAggregate;
using System.Linq;
using Xunit;

namespace Munchkin.Runtime.Tests.Entities.GameRoomAggregate
{
    public class GameRoomTests
    {
        [Fact]
        public async void Create_WithNotNullParameter_ShouldCreateGameRoomWithSinglePlayer()
        {
            // Arrange
            var gameRoom = new GameRoom();
            var players = await gameRoom.GetUsers();
            var isEmpty = await gameRoom.IsEmpty();

            // Assert
            Assert.NotNull(gameRoom);
            Assert.Empty(players);
            Assert.True(isEmpty);
        }

        [Fact]
        public async void JoinRoom_WithNullParameter_ShouldNotJoinTheRoom()
        {
            // Arrange
            var gameRoom = new GameRoom();
            var expectedResult = JoinRoomResult.InvalidUser;

            // Act
            var joinResponse = await gameRoom.JoinRoom(null);
            var players = await gameRoom.GetUsers();
            var isEmpty = await gameRoom.IsEmpty();

            // Assert
            Assert.Equal(expectedResult, joinResponse);
            Assert.Empty(players);
            Assert.True(isEmpty);
        }

        [Fact]
        public async void JoinRoom_WithNotNullParameter_ShouldJoinAndHaveExactlyTwoPlayers()
        {
            // Arrange
            var user = new User(1, "Johny Cash", true);
            var gameRoom = new GameRoom();
            var expectedResponse = JoinRoomResult.JoinedRoom;

            // Act
            var joinResponse = await gameRoom.JoinRoom(user);
            var players = await gameRoom.GetUsers();
            var isEmpty = await gameRoom.IsEmpty();

            // Assert
            Assert.Equal(expectedResponse, joinResponse);
            Assert.Single(players);
            Assert.False(isEmpty);
        }

        [Fact]
        public async void LeaveRoom_WithNullParameter_ShouldNotLeaveTheRoom()
        {
            // Arrange
            var gameRoom = new GameRoom();
            var expectedResponse = JoinRoomResult.InvalidUser;

            // Act
            var joinResponse = await gameRoom.LeaveRoom(null);
            var players = await gameRoom.GetUsers();
            var isEmpty = await gameRoom.IsEmpty();

            // Assert
            Assert.Equal(expectedResponse, joinResponse);
            Assert.Empty(players);
            Assert.True(isEmpty);
        }

        [Fact]
        public async void LeaveRoom_OnEmptyRoomWithUser_ShouldLeaveAndGameRoomBecomeEmpty()
        {
            // Arrange
            var user1 = new User(1, "Johny Cash", true);
            var gameRoom = new GameRoom();
            var expectedResponse = JoinRoomResult.RoomEmpty;

            // Act
            var leaveResponse = await gameRoom.LeaveRoom(user1);
            var players = await gameRoom.GetUsers();
            var isEmpty = await gameRoom.IsEmpty();

            // Assert
            Assert.Equal(expectedResponse, leaveResponse);
            Assert.Empty(players);
            Assert.True(isEmpty);
        }

        [Fact]
        public async void LeaveRoom_WithNotNullParameter_ShouldLeaveAndGameRoomBecomeEmpty()
        {
            // Arrange
            var user = new User(1, "Johny Cash", true);
            var gameRoom = new GameRoom();
            var expectedResponse = JoinRoomResult.LeftRoom;

            // Act
            _ = gameRoom.JoinRoom(user);
            var leaveResponse = await gameRoom.LeaveRoom(user);
            var players = await gameRoom.GetUsers();
            var isEmpty = await gameRoom.IsEmpty();

            // Assert
            Assert.Equal(expectedResponse, leaveResponse);
            Assert.Empty(players);
            Assert.True(isEmpty);
        }

        [Fact]
        public async void SelectExpansion_WithValidCode_ShouldBeInAvailableExpansionCollection()
        {
            // Arrange
            var gameRoom = new GameRoom();
            var expansionOption = new ExpansionOption("munchkin:one", "Munchkin");
            var avaialableExpansions = new ExpansionOption[] { expansionOption };
            var expectedResponse = SelectExpansionResult.OptionSelected;

            // Act
            _ = gameRoom.SetAvailableExpansions(avaialableExpansions);
            var selectedResponse = await gameRoom.SelectExpansion(expansionOption.Code);
            var selectedExpansions = await gameRoom.GetExpansionSelections();

            // Assert
            Assert.Equal(expectedResponse, selectedResponse);
            Assert.Single(selectedExpansions.Where(x => x.Selected));
        }

        [Fact]
        public async void UnselectExpansion_WithValidCode_ShouldHaveEmptyAvailableExpansionCollection()
        {
            // Arrange
            var gameRoom = new GameRoom();
            var expansionOption = new ExpansionOption("munchkin:one", "Munchkin");
            var avaialableExpansions = new ExpansionOption[] { expansionOption };
            var expectedResponse = SelectExpansionResult.OptionUnselected;

            // Act
            await gameRoom.SetAvailableExpansions(avaialableExpansions);
            _ = await gameRoom.SelectExpansion(expansionOption.Code);
            var selectedResponse = await gameRoom.UnselectExpansion(expansionOption.Code);
            var selectedExpansions = await gameRoom.GetExpansionSelections();

            // Assert
            Assert.Equal(expectedResponse, selectedResponse);
            Assert.Empty(selectedExpansions.Where(x => x.Selected));
        }
    }
}

using Munchkin.Runtime.Entities.GameRoomAggregate;
using Munchkin.Runtime.Entities.UserAggregate;
using System.Linq;
using Xunit;

namespace Munchkin.Runtime.Tests.Entities.GameRoomAggregate
{
    public class GameRoomTests
    {
        [Fact]
        public void Create_WithNotNullParameter_ShouldCreateGameRoomWithSinglePlayer()
        {
            // Arrange
            var gameRoom = new GameRoom();

            // Assert
            Assert.NotNull(gameRoom);
            Assert.Empty(gameRoom.Players);
            Assert.True(gameRoom.IsEmpty);
        }

        [Fact]
        public void JoinRoom_WithNullParameter_ShouldNotJoinTheRoom()
        {
            // Arrange
            var gameRoom = new GameRoom();
            var expectedResult = JoinRoomResult.InvalidUser;

            // Act
            var (result, joinResponse) = gameRoom.JoinRoom(null);

            // Assert
            Assert.Equal(expectedResult, joinResponse);
            Assert.Empty(result.Players);
            Assert.True(result.IsEmpty);
        }

        [Fact]
        public void JoinRoom_WithNotNullParameter_ShouldJoinAndHaveExactlyTwoPlayers()
        {
            // Arrange
            var user = new User(1, "Johny Cash");
            var gameRoom = new GameRoom();
            var expectedResponse = JoinRoomResult.JoinedRoom;

            // Act
            var (result, joinResponse) = gameRoom.JoinRoom(user);

            // Assert
            Assert.Equal(expectedResponse, joinResponse);
            Assert.Single(result.Players);
            Assert.False(result.IsEmpty);
        }

        [Fact]
        public void LeaveRoom_WithNullParameter_ShouldNotLeaveTheRoom()
        {
            // Arrange
            var gameRoom = new GameRoom();
            var expectedResponse = JoinRoomResult.InvalidUser;

            // Act
            var (result, joinResponse) = gameRoom.LeaveRoom(null);

            // Assert
            Assert.Equal(expectedResponse, joinResponse);
            Assert.Empty(result.Players);
            Assert.True(result.IsEmpty);
        }

        [Fact]
        public void LeaveRoom_OnEmptyRoomWithUser_ShouldLeaveAndGameRoomBecomeEmpty()
        {
            // Arrange
            var user1 = new User(1, "Johny Cash");
            var gameRoom = new GameRoom();
            var expectedResponse = JoinRoomResult.RoomEmpty;

            // Act
            var (result, leaveResponse) = gameRoom.LeaveRoom(user1);

            // Assert
            Assert.Equal(expectedResponse, leaveResponse);
            Assert.Empty(result.Players);
            Assert.True(result.IsEmpty);
        }

        [Fact]
        public void LeaveRoom_WithNotNullParameter_ShouldLeaveAndGameRoomBecomeEmpty()
        {
            // Arrange
            var user = new User(1, "Johny Cash");
            var gameRoom = new GameRoom();
            var expectedResponse = JoinRoomResult.LeftRoom;

            // Act
            var _ = gameRoom.JoinRoom(user);
            var (result, leaveResponse) = gameRoom.LeaveRoom(user);

            // Assert
            Assert.Equal(expectedResponse, leaveResponse);
            Assert.Empty(result.Players);
            Assert.True(result.IsEmpty);
        }

        [Fact]
        public void SelectExpansion_WithValidCode_ShouldBeInAvailableExpansionCollection()
        {
            // Arrange
            var gameRoom = new GameRoom();
            var expansionOption = new ExpansionOption("munchkin:one", "Munchkin");
            var avaialableExpansions = new ExpansionOption[] { expansionOption };
            var expectedResponse = SelectExpansionResult.OptionSelected;

            // Act
            var updated = gameRoom.SetAvailableExpansions(avaialableExpansions);
            var (result, selectedResponse) = updated.SelectExpansion(expansionOption.Code);

            // Assert
            Assert.Equal(expectedResponse, selectedResponse);
            Assert.Single(result.SelectedExpansions);
            Assert.Equal(expansionOption, result.SelectedExpansions.First());
        }

        [Fact]
        public void UnselectExpansion_WithValidCode_ShouldHaveEmptyAvailableExpansionCollection()
        {
            // Arrange
            var gameRoom = new GameRoom();
            var expansionOption = new ExpansionOption("munchkin:one", "Munchkin");
            var avaialableExpansions = new ExpansionOption[] { expansionOption };
            var expectedResponse = SelectExpansionResult.OptionUnselected;

            // Act
            var updated1 = gameRoom.SetAvailableExpansions(avaialableExpansions);
            var (updated2, _) = updated1.SelectExpansion(expansionOption.Code);
            var (result, selectedResponse) = updated2.UnselectExpansion(expansionOption.Code);

            // Assert
            Assert.Equal(expectedResponse, selectedResponse);
            Assert.Empty(result.SelectedExpansions);
        }
    }
}

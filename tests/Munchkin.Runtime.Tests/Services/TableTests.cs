using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using Munchkin.Core.Model.Expansions;
using Munchkin.Runtime.Services;
using System.Linq;
using Xunit;

namespace Munchkin.Runtime.Tests.Entities.GameRoomAggregate
{
    public class TableTests
    {
        [Fact]
        public async void Create_WithNotNullParameter_ShouldCreateGameRoomWithSinglePlayer()
        {
            // Arrange
            var table = new Services.Table();
            var players = await table.GetPlayers();

            // Assert
            Assert.NotNull(table);
            Assert.Empty(players);
        }

        [Fact]
        public async void JoinRoom_WithNullParameter_ShouldNotJoinTheRoom()
        {
            // Arrange
            var table = new Services.Table();
            var expectedResult = JoinTableResult.InvalidUser;

            // Act
            var joinResponse = await table.JoinRoom(null);
            var players = await table.GetPlayers();

            // Assert
            Assert.Equal(expectedResult, joinResponse);
            Assert.Empty(players);
        }

        [Fact]
        public async void JoinRoom_WithNotNullParameter_ShouldJoinAndHaveExactlyTwoPlayers()
        {
            // Arrange
            var player = new Player("johny.cash", EGender.Male);
            var table = new Services.Table();
            var expectedResponse = JoinTableResult.JoinedRoom;

            // Act
            var joinResponse = await table.JoinRoom(player);
            var players = await table.GetPlayers();

            // Assert
            Assert.Equal(expectedResponse, joinResponse);
            Assert.Single(players);
        }

        [Fact]
        public async void LeaveRoom_WithNullParameter_ShouldNotLeaveTheRoom()
        {
            // Arrange
            var table = new Services.Table();
            var expectedResponse = JoinTableResult.InvalidUser;

            // Act
            var joinResponse = await table.LeaveRoom(null);
            var players = await table.GetPlayers();

            // Assert
            Assert.Equal(expectedResponse, joinResponse);
            Assert.Empty(players);
        }

        [Fact]
        public async void LeaveRoom_OnEmptyRoomWithUser_ShouldLeaveAndGameRoomBecomeEmpty()
        {
            // Arrange
            var player = new Player("johny.cash", EGender.Male);
            var table = new Services.Table();
            var expectedResponse = JoinTableResult.RoomEmpty;

            // Act
            var leaveResponse = await table.LeaveRoom(player);
            var players = await table.GetPlayers();

            // Assert
            Assert.Equal(expectedResponse, leaveResponse);
            Assert.Empty(players);
        }

        [Fact]
        public async void LeaveRoom_WithNotNullParameter_ShouldLeaveAndGameRoomBecomeEmpty()
        {
            // Arrange
            var player = new Player("johny.cash", EGender.Male);
            var table = new Services.Table();
            var expectedResponse = JoinTableResult.LeftRoom;

            // Act
            _ = table.JoinRoom(player);
            var leaveResponse = await table.LeaveRoom(player);
            var players = await table.GetPlayers();

            // Assert
            Assert.Equal(expectedResponse, leaveResponse);
            Assert.Empty(players);
        }

        [Fact]
        public async void SelectExpansion_WithValidCode_ShouldBeInAvailableExpansionCollection()
        {
            // Arrange
            var table = new Services.Table();
            var expansionOption = new ExpansionOption("munchkin:one", "Munchkin");
            var avaialableExpansions = new ExpansionOption[] { expansionOption };
            var expectedResponse = SelectExpansionResult.OptionSelected;

            // Act
            _ = table.WithExpansions(avaialableExpansions);
            var selectedResponse = await table.SelectExpansion(expansionOption.Code);
            var selectedExpansions = await table.GetExpansionSelections();

            // Assert
            Assert.Equal(expectedResponse, selectedResponse);
            Assert.Single(selectedExpansions.Where(x => x.Selected));
        }

        [Fact]
        public async void UnselectExpansion_WithValidCode_ShouldHaveEmptyAvailableExpansionCollection()
        {
            // Arrange
            var table = new Services.Table();
            var expansionOption = new ExpansionOption("munchkin:one", "Munchkin");
            var avaialableExpansions = new ExpansionOption[] { expansionOption };
            var expectedResponse = SelectExpansionResult.OptionUnselected;

            // Act
            await table.WithExpansions(avaialableExpansions);
            _ = await table.SelectExpansion(expansionOption.Code);
            var selectedResponse = await table.UnselectExpansion(expansionOption.Code);
            var selectedExpansions = await table.GetExpansionSelections();

            // Assert
            Assert.Equal(expectedResponse, selectedResponse);
            Assert.Empty(selectedExpansions.Where(x => x.Selected));
        }
    }
}

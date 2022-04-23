using MediatR;
using Moq;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Expansions;
using Munchkin.Runtime.Abstractions;
using Munchkin.Runtime.Services;
using Orleans.Runtime;
using System;
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
            var table = CreateTable();
            var players = await table.GetPlayersAsync();

            // Assert
            Assert.NotNull(table);
            Assert.Empty(players);
        }

        [Fact]
        public async void JoinRoom_WithNullParameter_ShouldNotJoinTheRoom()
        {
            // Arrange
            var table = CreateTable();
            var expectedResult = JoinTableResult.InvalidUser;

            // Act
            var joinResponse = await table.JoinAsync(null);
            var players = await table.GetPlayersAsync();

            // Assert
            Assert.Equal(expectedResult, joinResponse);
            Assert.Empty(players);
        }

        [Fact]
        public async void JoinRoom_WithNotNullParameter_ShouldJoinAndHaveExactlyTwoPlayers()
        {
            // Arrange
            var player = new Player("johny.cash", EGender.Male);
            var table = CreateTable();
            var expectedResponse = JoinTableResult.JoinedRoom;

            // Act
            var joinResponse = await table.JoinAsync(player);
            var players = await table.GetPlayersAsync();

            // Assert
            Assert.Equal(expectedResponse, joinResponse);
            Assert.Single(players);
        }

        [Fact]
        public async void LeaveRoom_WithNullParameter_ShouldNotLeaveTheRoom()
        {
            // Arrange
            var table = CreateTable();
            var expectedResponse = JoinTableResult.InvalidUser;

            // Act
            var joinResponse = await table.LeaveAsync(null);
            var players = await table.GetPlayersAsync();

            // Assert
            Assert.Equal(expectedResponse, joinResponse);
            Assert.Empty(players);
        }

        [Fact]
        public async void LeaveRoom_OnEmptyRoomWithUser_ShouldLeaveAndGameRoomBecomeEmpty()
        {
            // Arrange
            var player = new Player("johny.cash", EGender.Male);
            var table = CreateTable();
            var expectedResponse = JoinTableResult.RoomEmpty;

            // Act
            var leaveResponse = await table.LeaveAsync(player);
            var players = await table.GetPlayersAsync();

            // Assert
            Assert.Equal(expectedResponse, leaveResponse);
            Assert.Empty(players);
        }

        [Fact]
        public async void LeaveRoom_WithNotNullParameter_ShouldLeaveAndGameRoomBecomeEmpty()
        {
            // Arrange
            var player = new Player("johny.cash", EGender.Male);
            var table = CreateTable();
            var expectedResponse = JoinTableResult.LeftRoom;

            // Act
            _ = table.JoinAsync(player);
            var leaveResponse = await table.LeaveAsync(player);
            var players = await table.GetPlayersAsync();

            // Assert
            Assert.Equal(expectedResponse, leaveResponse);
            Assert.Empty(players);
        }

        [Fact]
        public async void SelectExpansion_WithValidCode_ShouldBeInAvailableExpansionCollection()
        {
            // Arrange
            var table = CreateTable();
            var expansionOption = new ExpansionOption("munchkin:one", "Munchkin");
            var avaialableExpansions = new ExpansionOption[] { expansionOption };
            var expectedResponse = SelectExpansionResult.OptionSelected;

            // Act
            table = await table.SetupAsync();
            var selectedResponse = await table.IncludeExpansionAsync(expansionOption.Code);
            var selectedExpansions = await table.GetIncludedExpansionsAsync();

            // Assert
            Assert.Equal(expectedResponse, selectedResponse);
            Assert.Single(selectedExpansions.Where(x => x.Selected));
        }

        [Fact]
        public async void UnselectExpansion_WithValidCode_ShouldHaveEmptyAvailableExpansionCollection()
        {
            // Arrange
            var table = CreateTable();
            var expansionOption = new ExpansionOption("munchkin:one", "Munchkin");
            var avaialableExpansions = new ExpansionOption[] { expansionOption };
            var expectedResponse = SelectExpansionResult.OptionUnselected;

            // Act
            table = await table.SetupAsync();
            var response = await table.IncludeExpansionAsync(expansionOption.Code);
            var selectedResponse = await table.ExcludeExpansionAsync(expansionOption.Code);
            var selectedExpansions = await table.GetIncludedExpansionsAsync();

            // Assert
            Assert.Equal(expectedResponse, selectedResponse);
            Assert.Empty(selectedExpansions.Where(x => x.Selected));
        }

        private static ITable CreateTable()
        {
            var persistence = Mock.Of<IPersistentState<Table>>();
            var mediator = Mock.Of<IMediator>();
            var services = Mock.Of<IServiceProvider>();
            var table = new TableGrain(persistence, mediator, services);
            return table;
        }
    }
}

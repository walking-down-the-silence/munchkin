using FluentAssertions;
using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards.Doors.Curses;
using Munchkin.Core.Model.Cards.Doors.Enhancers;
using Munchkin.Core.Model.Cards.Doors.Monsters;
using Munchkin.Core.Model.Cards.Treasures.OneShot;
using Munchkin.Core.Model.Phases;
using System.Linq;
using Xunit;

namespace Munchkin.Core.Tests.Model.Phases
{
    public class DungeonTests
    {
        [Fact]
        public void KickOpenTheDoor_WithCurse_ShouldHaveCursedRoomState()
        {
            // Arrange
            var doorCards = Enumerable.Repeat(new ChangeClass(), 10).ToArray();
            var treasureCards = Enumerable.Repeat(new CotionOfPonfusion(), 10).ToArray();
            var player = new Player("johny.cash", EGender.Male);
            var table = Table.Empty()
                .WithWinningLevel(10)
                .WithDoorDeck(doorCards)
                .WithTreasureDeck(treasureCards);
            var joined = table.Join(player);

            // Act
            var nextState = Dungeon.KickOpenTheDoor(table);

            // Assert
            nextState.Should().NotBeNull();
            nextState.Should().BeOfType<Table>();
        }

        [Fact]
        public void KickOpenTheDoor_WithMonster_ShouldHaveCombatRoomState()
        {
            // Arrange
            var doorCards = Enumerable.Repeat(new PlutoniumDragon(), 10).ToArray();
            var treasureCards = Enumerable.Repeat(new CotionOfPonfusion(), 10).ToArray();
            var player = new Player("johny.cash", EGender.Male);
            var table = Table.Empty()
                .WithWinningLevel(10)
                .WithDoorDeck(doorCards)
                .WithTreasureDeck(treasureCards);
            var joined = table.Join(player);

            // Act
            var nextState = Dungeon.KickOpenTheDoor(table);

            // Assert
            nextState.Should().NotBeNull();
            nextState.Should().BeOfType<Table>();
        }

        [Fact]
        public void KickOpenTheDoor_WithEnhancer_ShouldHaveEmptyRoomState()
        {
            // Arrange
            var doorCards = Enumerable.Repeat(new Ancient(), 10).ToArray();
            var treasureCards = Enumerable.Repeat(new CotionOfPonfusion(), 10).ToArray();
            var player = new Player("johny.cash", EGender.Male);
            var table = Table.Empty()
                .WithWinningLevel(10)
                .WithDoorDeck(doorCards)
                .WithTreasureDeck(treasureCards);
            var joined = table.Join(player);

            // Act
            var nextState = Dungeon.KickOpenTheDoor(table);

            // Assert
            nextState.Should().NotBeNull();
            nextState.Should().BeOfType<Table>();
        }

        [Fact]
        public void LootTheRoom_ShouldAddOneMoreDoorToPlayersHand()
        {
            // Arrange
            var doorCards = Enumerable.Repeat(new Ancient(), 10).ToArray();
            var treasureCards = Enumerable.Repeat(new CotionOfPonfusion(), 10).ToArray();
            var player = new Player("johny.cash", EGender.Male);
            var table = Table.Empty()
                .WithWinningLevel(10)
                .WithDoorDeck(doorCards)
                .WithTreasureDeck(treasureCards);
            var joined = table.Join(player);

            // Act
            var nextState = Dungeon.LootTheRoom(table);

            // Assert
            nextState.Should().NotBeNull();
            nextState.Should().BeOfType<Table>();
            table.Players.Current.Should().NotBeNull();
            table.Players.Current.Should().BeSameAs(player);
            player.YourHand.Should().NotBeNull();
            player.YourHand.Should().HaveCount(1);
        }

        [Fact]
        public void LookForTrouble_ShouldReturnLookForTroubleState()
        {
            // Arrange
            var doorCards = Enumerable.Repeat(new PlutoniumDragon(), 10).ToArray();
            var treasureCards = Enumerable.Repeat(new CotionOfPonfusion(), 10).ToArray();
            var player = new Player("johny.cash", EGender.Male);
            var table = Table.Empty()
                .WithWinningLevel(10)
                .WithDoorDeck(doorCards)
                .WithTreasureDeck(treasureCards);
            var monster = doorCards.First();
            var joined = table.Join(player);

            // Act
            player.TakeInHand(monster);
            var nextState = Dungeon.LookForTrouble(table, monster);

            // Assert
            nextState.Should().NotBeNull();
            nextState.Should().BeOfType<Table>();
            table.Players.Current.Should().NotBeNull();
            table.Players.Current.Should().BeSameAs(player);
            player.YourHand.Should().NotBeNull();
            player.YourHand.Should().BeEmpty();
        }
    }
}

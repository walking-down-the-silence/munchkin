using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using System.Linq;
using Xunit;

namespace Munchkin.Core.Tests.Model
{
    public class TableTests
    {
        [Fact]
        public void Empty_ShouldHaveEmptyDecks()
        {
            // Arrange
            var table = Table.Empty();

            // Assert
            Assert.Empty(table.Players);
            Assert.Empty(table.TreasureCardDeck);
            Assert.Empty(table.DoorsCardDeck);
            Assert.Empty(table.DiscardedTreasureCards);
            Assert.Empty(table.DiscardedDoorsCards);
        }

        [Fact]
        public void JoinPlayer_WithTwoPlayers_ShouldHaveNotEmptyPlayerList()
        {
            // Arrange
            var player1 = new Player("Frank Sinatra", EGender.Male);
            var player2 = new Player("Johny Cash", EGender.Male);
            var table = Table.Empty();

            // Act
            table.JoinPlayer(player1);
            table.JoinPlayer(player2);

            // Assert
            Assert.Equal(2, table.Players.Count);
            Assert.Empty(table.TreasureCardDeck);
            Assert.Empty(table.DoorsCardDeck);
            Assert.Empty(table.DiscardedTreasureCards);
            Assert.Empty(table.DiscardedDoorsCards);
        }

        [Fact]
        public void AddTreasures_WithTwoPlayers_ShouldHaveNotEmptyTreasureDeck()
        {
            // Arrange
            var treasureFactory = new MunchkinOriginalTreasuresFactory();
            var table = Table.Empty();

            // Act
            table.AddTreasures(treasureFactory.GetTreasureCards().ToArray());

            // Assert
            Assert.NotEmpty(table.TreasureCardDeck);
            Assert.Empty(table.DoorsCardDeck);
            Assert.Empty(table.DiscardedTreasureCards);
            Assert.Empty(table.DiscardedDoorsCards);
        }

        [Fact]
        public void AddDoors_WithTwoPlayers_ShouldHaveNotEmptyDoorDeck()
        {
            // Arrange
            var doorFactory = new MunchkinOriginalDoorsFactory();
            var table = Table.Empty();

            // Act
            table.AddDoors(doorFactory.GetDoorsCards().ToArray());

            // Assert
            Assert.Empty(table.TreasureCardDeck);
            Assert.NotEmpty(table.DoorsCardDeck);
            Assert.Empty(table.DiscardedTreasureCards);
            Assert.Empty(table.DiscardedDoorsCards);
        }
    }
}

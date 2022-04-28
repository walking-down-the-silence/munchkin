using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using System.Collections.Generic;
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
            var players = new List<Player>
            {
                player1,
                player2
            };
            var table = Table.Empty();

            // Act
            players.ForEach(player => table.Join(player));

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
            Table result = table.WithTreasureDeck(treasureFactory.GetTreasureCards().ToArray());

            // Assert
            Assert.NotNull(result);
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
            Table result = table.WithDoorDeck(doorFactory.GetDoorsCards().ToArray());

            // Assert
            Assert.NotNull(result);
            Assert.Empty(table.TreasureCardDeck);
            Assert.NotEmpty(table.DoorsCardDeck);
            Assert.Empty(table.DiscardedTreasureCards);
            Assert.Empty(table.DiscardedDoorsCards);
        }
    }
}

using MediatR;
using Moq;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using System;
using System.Linq;
using Xunit;

namespace Munchkin.Core.Tests.Model
{
    public class TableTests
    {
        [Fact]
        public void Ctor_WithNullParameters_ThrowsArgumentNullException()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => new Table(null));
        }

        [Fact]
        public void Ctor_WithNotNullParameters_ShouldHaveEmptyDecks()
        {
            // Arrange
            var mediator = Mock.Of<IMediator>();
            var table = new Table(mediator);

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
            var mediator = Mock.Of<IMediator>();
            var player1 = new Player("Frank Sinatra", EGender.Male);
            var player2 = new Player("Johny Cash", EGender.Male);
            var treasureFactory = new MunchkinOriginalTreasuresFactory();
            var doorFactory = new MunchkinOriginalDoorsFactory();
            var table = new Table(mediator);

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
            var mediator = Mock.Of<IMediator>();
            var treasureFactory = new MunchkinOriginalTreasuresFactory();
            var table = new Table(mediator);

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
            var mediator = Mock.Of<IMediator>();
            var doorFactory = new MunchkinOriginalDoorsFactory();
            var table = new Table(mediator);

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

using MediatR;
using Moq;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using Munchkin.Engine.Original;
using System;
using Xunit;

namespace Munchkin.Core.Tests.Model
{
    public class TableTests
    {
        [Fact]
        public void Setup_WithNullParameters_ThrowsArgumentNullException()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => Table.Setup(null, null, null, null, 0));
        }

        [Fact]
        public void SetupWithNotNullParameters_ShouldHaveNotEmptyDecks()
        {
            // Arrange
            var mediator = Mock.Of<IMediator>();
            var players = new Player[]
            {
                new Player("Frank Sinatra", EGender.Male),
                new Player("Johny Cash", EGender.Male)
            };
            var treasureFactory = new MunchkinOriginalTreasuresFactory();
            var doorFactory = new MunchkinOriginalDoorsFactory();
            var table = Table.Setup(mediator, players, treasureFactory, doorFactory, 10);

            // Assert
            Assert.NotEmpty(table.Players);
            Assert.NotEmpty(table.TreasureCardDeck);
            Assert.NotEmpty(table.DoorsCardDeck);
            Assert.Empty(table.DiscardedTreasureCards);
            Assert.Empty(table.DiscardedDoorsCards);
        }
    }
}

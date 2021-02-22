using MediatR;
using Moq;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;
using Munchkin.Expansions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            var treasureFactory = new TreasuresFactoryFake();
            var doorFactory = new DoorsFactoryFake();
            var table = Table.Setup(mediator, players, treasureFactory, doorFactory, 10);

            // Assert
            Assert.NotEmpty(table.Players);
            Assert.NotEmpty(table.TreasureCardDeck);
            Assert.NotEmpty(table.DoorsCardDeck);
            Assert.Empty(table.DiscardedTreasureCards);
            Assert.Empty(table.DiscardedDoorsCards);
        }

        private class DoorsFactoryFake : IDoorsFactory
        {
            public IEnumerable<DoorsCard> GetDoorsCards()
            {
                yield return new SuperMunchkin();
                yield return new SuperMunchkin();
                yield return new SuperMunchkin();
                yield return new SuperMunchkin();
                yield return new SuperMunchkin();
                yield return new SuperMunchkin();
                yield return new SuperMunchkin();
                yield return new SuperMunchkin();
                yield return new SuperMunchkin();
                yield return new SuperMunchkin();
            }
        }

        private class TreasuresFactoryFake : ITreasuresFactory
        {
            public IEnumerable<TreasureCard> GetTreasureCards()
            {
                yield return new TreasureCardFake();
                yield return new TreasureCardFake();
                yield return new TreasureCardFake();
                yield return new TreasureCardFake();
                yield return new TreasureCardFake();
                yield return new TreasureCardFake();
                yield return new TreasureCardFake();
                yield return new TreasureCardFake();
                yield return new TreasureCardFake();
                yield return new TreasureCardFake();
            }
        }

        private class TreasureCardFake : TreasureCard
        {
            public TreasureCardFake() : base("Treasure Fake")
            {
            }

            public override Task Play(Table context) => Task.CompletedTask;
        }
    }
}

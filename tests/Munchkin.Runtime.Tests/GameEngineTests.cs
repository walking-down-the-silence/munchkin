using MediatR;
using Moq;
using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using System.Collections.Generic;
using Xunit;

namespace Munchkin.Runtime.Tests
{
    public class GameEngineTests
    {
        [Fact]
        public async void RunAsync()
        {
            // Arrange
            var mediatr = Mock.Of<IMediator>();

            var player1 = new Player("Bill Gates", EGender.Male);
            var player2 = new Player("Elon Musk", EGender.Male);
            var player3 = new Player("Jeff Bezos", EGender.Male);
            var players = new Player[]
            {
                player1,
                player2,
                player3
            };

            var expansion1 = new MunchkinOriginalExpansion();
            var expansions = new IExpansion[] { expansion1 };

            var gameEngine = new GameEngine(mediatr, expansions, players);

            // Act
            //var result = await gameEngine.RunAsync();

            // Assert
            //Assert.NotNull(result);
        }

        private class MunchkinOriginalExpansion : IExpansion
        {
            public string Code => "munchkin.original";

            public string Title => "Munchkin";

            public IDoorDeckFactory DoorDeck => new MunchkinOriginalDoordeckFactory();

            public ITreasureDeckFactory TreasureDeck => new MunchkinOriginalTreasureDeckFactory();
        }

        private class MunchkinOriginalDoordeckFactory : IDoorDeckFactory
        {
            public IReadOnlyCollection<DoorsCard> GetDoorsCards()
            {
                throw new System.NotImplementedException();
            }
        }

        private class MunchkinOriginalTreasureDeckFactory : ITreasureDeckFactory
        {
            public IReadOnlyCollection<TreasureCard> GetTreasureCards()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}

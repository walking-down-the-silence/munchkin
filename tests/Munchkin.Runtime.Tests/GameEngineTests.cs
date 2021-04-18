using MediatR;
using Moq;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
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

            var expansion1 = new MunchkinOriginal();
            var expansions = new IExpansion[] { expansion1 };

            var gameEngine = new GameEngine(mediatr, expansions, players);

            // Act
            //var result = await gameEngine.RunAsync();

            // Assert
            //Assert.NotNull(result);
        }
    }
}

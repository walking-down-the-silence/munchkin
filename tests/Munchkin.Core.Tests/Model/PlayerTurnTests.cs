using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using Xunit;

namespace Munchkin.Core.Tests
{
    public class PlayerTurnTests
    {
        [Fact]
        public async void Test1()
        {
            // Arrange
            var players = new Player[]
            {
                new Player("Johny Cash", EGender.Male),
                new Player("Frank Sinatra", EGender.Male),
                new Player("Marie Curie", EGender.Female)
            };

            // Act
            await PlayerTurn.Start(players, 10);
        }
    }
}

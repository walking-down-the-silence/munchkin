using FluentAssertions;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards.Doors;
using Munchkin.Core.Model.Cards.Doors.Classes;
using Xunit;

namespace Munchkin.Core.Tests.Model.Cards.Doors
{
    public class DivineInterventionTests
    {
        [Fact]
        public async void Play__OnTableWithTwoPlayersAsClerics__ShouldLevelUpBothPlayers()
        {
            // Arrange
            var table = Table.Empty();
            var playerJohny = new Player("johny.cash", EGender.Male);
            var playerFrank = new Player("frank.sinatra", EGender.Male);
            var cleric1 = new ClericClass();
            var cleric2 = new ClericClass();
            var card = new DivineIntervention();

            // Act
            cleric1.Equip(table, playerJohny);
            cleric2.Equip(table, playerFrank);

            var (updated1, result1) = table.Join(playerJohny);
            var (updated2, result2) = updated1.Join(playerFrank);

            await card.Play(updated2);

            // Arrange
            playerJohny.Level.Should().Be(2);
            playerFrank.Level.Should().Be(2);
        }
    }
}

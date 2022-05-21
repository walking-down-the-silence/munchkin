using FluentAssertions;
using Munchkin.Core.Contracts.Exceptions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards.Doors.Classes;
using Munchkin.Core.Model.Cards.Treasures.Permanent;
using Xunit;

namespace Munchkin.Core.Tests.Model.Cards.Treasures
{
    public class CloakOfObscurityTests
    {
        [Fact]
        public void Equip__OnPlayerWithoutThiefClass__ShouldThrow_CardCannotBeEquippedException()
        {
            // Arrange
            var player1 = new Player("johny.cash", Contracts.EGender.Male);
            var player2 = new Player("frank.sinatra", Contracts.EGender.Male);
            var table = Table.Empty();
            var wearingCard = new CloakOfObscurity();

            // Act
            var (updated1, result1) = table.Join(player1);
            var equipActionException = Record.Exception(() => wearingCard.Equip(updated1, player1));

            // Assert
            equipActionException.Should().NotBeNull();
            equipActionException.Should().BeOfType<CardCannotBeEquippedException>();
        }

        [Fact]
        public void Equip__OnPlayerWithThiefClass__ShouldHave_CloakOfObscurityEquipped()
        {
            // Arrange
            var player1 = new Player("johny.cash", Contracts.EGender.Male);
            var player2 = new Player("frank.sinatra", Contracts.EGender.Male);
            var table = Table.Empty();
            var classCard = new ThiefClass();
            var wearingCard = new CloakOfObscurity();

            // Act
            var (updated1, result1) = table.Join(player1);
            classCard.Equip(updated1, player1);
            wearingCard.Equip(updated1, player1);

            // Assert
            classCard.Owner.Should().BeSameAs(player1);
            wearingCard.Owner.Should().BeSameAs(player1);
            player1.Equipped.Should().Contain(classCard);
            player1.Equipped.Should().Contain(wearingCard);
        }
    }
}

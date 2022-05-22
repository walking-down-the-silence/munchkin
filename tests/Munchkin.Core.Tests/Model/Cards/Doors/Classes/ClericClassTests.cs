using FluentAssertions;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards.Doors;
using Munchkin.Core.Model.Cards.Doors.Classes;
using System;
using Xunit;

namespace Munchkin.Core.Tests.Model.Cards.Doors.Classes
{
    public class ClericClassTests
    {
        [Fact]
        public void TakenBy__OnPlayerNull__ShouldThrowArgumentNullException()
        {
            // Arrange
            var table = Table.Empty();
            var playerJohny = new Player("johny.cash", Contracts.EGender.Male);
            var clericClass = new ClericClass();

            // Act
            var argumentNullException = Record.Exception(() => clericClass.TakenBy(null));

            // Assert
            argumentNullException.Should().NotBeNull();
            argumentNullException.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public void TakenBy__OnPlayer__ShouldHaveOwnerNotNull()
        {
            // Arrange
            var playerJohny = new Player("johny.cash", Contracts.EGender.Male);
            var clericClass = new ClericClass();

            // Act
            clericClass.TakenBy(playerJohny);

            // Assert
            clericClass.Owner.Should().NotBeNull();
            clericClass.Owner.Should().BeSameAs(playerJohny);
        }

        [Fact]
        public void Bind__OnNull__ShouldThrow_ArgumentNullException()
        {
            // Arrange
            var clericClass = new ClericClass();

            // Act
            var argumentNullException = Record.Exception(() => clericClass.Bind(null));

            // Assert
            argumentNullException.Should().NotBeNull();
            argumentNullException.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public void Bind__OnSuperMunchkinCard__ShouldHave_BoundToNotNull()
        {
            // Arrange
            var clericClass = new ClericClass();
            var superMunchkin = new SuperMunchkin();

            // Act
            clericClass.Bind(superMunchkin);

            // Assert
            clericClass.BoundTo.Should().BeNull();
            clericClass.BoundCards.Should().Contain(superMunchkin);
            superMunchkin.BoundTo.Should().BeSameAs(clericClass);
        }

        [Fact]
        public void Discard__OnEmptyTable__Should_NotBeBound_AndNotHaveBoundedCards_AndNotHaveAnOwner()
        {
            // Arrange
            var table = Table.Empty();
            var playerJohny = new Player("johny.cash", Contracts.EGender.Male);
            var clericClass = new ClericClass();
            var superMunchkin = new SuperMunchkin();

            // Act
            clericClass.TakenBy(playerJohny);
            clericClass.Bind(superMunchkin);
            clericClass.Discard(table);

            // Assert
            clericClass.Owner.Should().BeNull();
            clericClass.BoundTo.Should().BeNull();
            clericClass.BoundCards.Should().BeEmpty();
            superMunchkin.BoundTo.Should().BeNull();
            playerJohny.AllCards().Should().BeEmpty();
        }

        [Fact]
        public void Equip__OnEmptyTableAndNullPlayer__ShouldThrow_ArgumentNullException()
        {
            // Arrange
            var table = Table.Empty();
            var clericClass = new ClericClass();

            // Act
            var argumentNullException = Record.Exception(() => clericClass.Equip(table, null));

            // Assert
            argumentNullException.Should().NotBeNull();
            argumentNullException.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public void Equip__OnNullTableAndNotNullPlayer__ShouldThrow_ArgumentNullException()
        {
            // Arrange
            var playerJohny = new Player("johny.cash", Contracts.EGender.Male);
            var clericClass = new ClericClass();

            // Act
            var argumentNullException = Record.Exception(() => clericClass.Equip(null, playerJohny));

            // Assert
            argumentNullException.Should().NotBeNull();
            argumentNullException.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public void Equip__OnEmptyTableAndNotNullPlayer__ShouldHave_ClericEquipped()
        {
            // Arrange
            var table = Table.Empty();
            var playerJohny = new Player("johny.cash", Contracts.EGender.Male);
            var clericClass = new ClericClass();

            // Act
            clericClass.Equip(table, playerJohny);

            // Assert
            playerJohny.Equipped.Should().Contain(clericClass);
        }
    }
}

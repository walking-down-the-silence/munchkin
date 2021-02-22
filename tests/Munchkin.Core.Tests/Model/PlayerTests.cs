using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using System;
using Xunit;

namespace Munchkin.Core.Tests.Model
{
    public class PlayerTests
    {
        [Fact]
        public void Ctor_WithWhitespaceName_ThorwsArgumentException()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() => new Player(string.Empty, EGender.Male));
        }

        [Fact]
        public void Ctor_WithValidName_ShouldCreatePlayer()
        {
            // Arrange
            var player = new Player("Johny Cash", EGender.Male);

            // Act, Assert
            Assert.NotNull(player.Name);
            Assert.Equal(EGender.Male, player.Gender);
            Assert.Equal(1, player.Level);
            Assert.False(player.IsDead);
            Assert.Empty(player.YourHand);
            Assert.Empty(player.Equipped);
            Assert.Empty(player.Backpack);
        }

        [Fact]
        public void TakeInHand_WithNull_ShouldHaveEmptyHand()
        {
            // Arrange
            var player = new Player("Johny Cash", EGender.Male);

            // Act
            player.TakeInHand(null);

            // Assert
            Assert.Empty(player.YourHand);
            Assert.Empty(player.Equipped);
            Assert.Empty(player.Backpack);
        }

        [Fact]
        public void TakeInHand_WithValidCard_ShouldNotHaveEmptyHand()
        {
            // Arrange
            var player = new Player("Johny Cash", EGender.Male);
            var card = new SuperMunchkin();

            // Act
            player.TakeInHand(card);

            // Assert
            Assert.NotEmpty(player.YourHand);
            Assert.Empty(player.Equipped);
            Assert.Empty(player.Backpack);
        }

        [Fact]
        public void PutInPlayAsEquipped_WithNull_ShouldNotHaveEmptyEquipped()
        {
            // Arrange
            var player = new Player("Johny Cash", EGender.Male);

            // Act
            player.PutInPlayAsEquipped(null);

            // Assert
            Assert.Empty(player.YourHand);
            Assert.Empty(player.Equipped);
            Assert.Empty(player.Backpack);
        }

        [Fact]
        public void PutInPlayAsEquipped_WithValidCard_ShouldNotHaveEmptyEquipped()
        {
            // Arrange
            var player = new Player("Johny Cash", EGender.Male);
            var card = new SuperMunchkin();

            // Act
            player.TakeInHand(card);
            player.PutInPlayAsEquipped(card);

            // Assert
            Assert.Empty(player.YourHand);
            Assert.NotEmpty(player.Equipped);
            Assert.Empty(player.Backpack);
        }

        [Fact]
        public void PutInPlayAsCarried_WithNull_ShouldNotHaveEmptyBackpack()
        {
            // Arrange
            var player = new Player("Johny Cash", EGender.Male);

            // Act
            player.PutInPlayAsCarried(null);

            // Assert
            Assert.Empty(player.YourHand);
            Assert.Empty(player.Equipped);
            Assert.Empty(player.Backpack);
        }

        [Fact]
        public void PutInPlayAsCarried_WithValidCard_ShouldNotHaveEmptyBackpack()
        {
            // Arrange
            var player = new Player("Johny Cash", EGender.Male);
            var card = new SuperMunchkin();

            // Act
            player.TakeInHand(card);
            player.PutInPlayAsCarried(card);

            // Assert
            Assert.Empty(player.YourHand);
            Assert.Empty(player.Equipped);
            Assert.NotEmpty(player.Backpack);
        }

        [Fact]
        public void LevelDown_ShouldNotGoLowerThanLevel1()
        {
            // Arrange
            var player = new Player("Johny Cash", EGender.Male);

            // Act
            player.LevelDown();
            player.LevelDown();
            player.LevelDown();

            // Assert
            Assert.Equal(1, player.Level);
        }

        [Fact]
        public void LevelUp_ShouldGoUpOneLevel()
        {
            // Arrange
            var player = new Player("Johny Cash", EGender.Male);

            // Act
            player.LevelUp();

            // Assert
            Assert.Equal(2, player.Level);
        }
    }
}

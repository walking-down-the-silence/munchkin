using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using Munchkin.Core.Model.Stages;
using System;
using Xunit;

namespace Munchkin.Core.Tests.Model.Stages
{
    public class KickOpenTheDoorStepTests
    {
        [Fact]
        public void Ctor_WithNullParameter_SholdThrowArgumentNullExpection()
        {
            // Act
            Player player = null;

            // Act
            var exception = Record.Exception(() => new KickOpenTheDoorStep(player));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
            Assert.Equal("Value cannot be null. (Parameter 'currentPlayer')", exception.Message);
        }

        [Fact]
        public void Ctor_WithNotNullParameter_SholdNotThrowArgumentNullExpection()
        {
            // Arrange
            var player = CreatePlayerJohny();

            // Act
            var exception = Record.Exception(() => new KickOpenTheDoorStep(player));

            // Act, Assert
            Assert.Null(exception);
        }

        [Fact]
        public void OnResolve_WhenCardIsNotAMonsterAndIsNotACurse_ShouldTakeCardInHand()
        {
            // Arrange
            var player = CreatePlayerJohny();

            // Act
            // Assert
        }

        [Fact]
        public void OnResolve_WhenCardIsAMonsterAndIsNotACurse_ShouldPutInPlay()
        {
            // Arrange
            // Act
            // Assert
        }

        [Fact]
        public void OnResolve_WhenCardIsNotAMonsterButIsACurse_ShouldTakeConsequences()
        {
            // Arrange
            // Act
            // Assert
        }

        private static Player CreatePlayerJohny()
        {
            return new Player("Johny Cash", EGender.Male);
        }
    }
}

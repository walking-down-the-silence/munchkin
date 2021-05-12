using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using Munchkin.Core.Model.Stages;
using Munchkin.Engine.Original.Doors;
using System;
using System.Collections.Generic;
using Xunit;

namespace Munchkin.Core.Tests.Model.Stages
{
    public class RunAwayStepTests
    {
        // Questions:
        // 1) When there are only one player that is running away (I guess we should fix ctor and make HelpingPlayer not mandatory
        // 2) Dice.Roll() is random, thats the problem ( We should be able to mock this, but game should be automatic here)
        // 3) I have looked to the diagram of the game flow and it seems that we are not handling run away from all mosters or I am mistaken?
        // 4) Why to do double TakeBadStuff(); within the logic?

        [Fact]
        public void Ctor_WithFightingPlayerNullParameter_SholdThrowArgumentNullExpection()
        {
            // Arrange
            Player fightingPlayer = null;
            Player helpingPlayer = new("Johny Cash", EGender.Male);
            var mosterCard = new BandOf3872Orcs();
            var monsters = new List<MonsterCard>() { mosterCard };

            // Act
            var exception = Record.Exception(() => new RunAwayStep(fightingPlayer, helpingPlayer, monsters));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
            Assert.Equal("Value cannot be null. (Parameter 'fightingPlayer')", exception.Message);
        }

        [Fact]
        public void Ctor_WithHelpingPlayerNullParameter_SholdThrowArgumentNullExpection()
        {
            // Arrange
            Player fightingPlayer = new("Johny Cash", EGender.Male);
            Player helpingPlayer = null;
            var mosterCard = new BandOf3872Orcs();
            var monsters = new List<MonsterCard>() { mosterCard };

            // Act
            var exception = Record.Exception(() => new RunAwayStep(fightingPlayer, helpingPlayer, monsters));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
            Assert.Equal("Value cannot be null. (Parameter 'helpingPlayer')", exception.Message);
        }

        [Fact]
        public void Ctor_WithMostersNullParameter_SholdThrowArgumentNullExpection()
        {
            // Arrange
            Player fightingPlayer = new("Johny Cash", EGender.Male);
            Player helpingPlayer = new("Bruce Willis", EGender.Male);
            var mosterCard = new BandOf3872Orcs();
            List<MonsterCard> monsters = null;

            // Act
            var exception = Record.Exception(() => new RunAwayStep(fightingPlayer, helpingPlayer, monsters));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
            Assert.Equal("Value cannot be null. (Parameter 'monsters')", exception.Message);
        }

        [Fact]
        public void Ctor_WithNotNullParameter_SholdNotThrowArgumentNullExpection()
        {
            // Arrange
            Player fightingPlayer = new("Johny Cash", EGender.Male);
            Player helpingPlayer = new("Bruce Willis", EGender.Male);
            var mosterCard = new BandOf3872Orcs();
            var monsters = new List<MonsterCard>() { mosterCard };

            // Act
            var exception = Record.Exception(() => new RunAwayStep(fightingPlayer, helpingPlayer, monsters));

            // Act, Assert
            Assert.Null(exception);
        }
    }
}

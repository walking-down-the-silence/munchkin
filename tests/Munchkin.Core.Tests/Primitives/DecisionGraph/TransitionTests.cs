using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards.Doors.Monsters;
using Munchkin.Core.Model.Phases;
using Munchkin.Core.Primitives;
using System;
using Xunit;

namespace Munchkin.Core.Tests.Primitives
{
    public class TransitionTests
    {
        [Fact]
        public void Create_WithNullParameters_ShouldThrowArgumentNullException()
        {
            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => Transition.Create<KickOpenTheDoorStep, EmptyRoomStep>(null, null));
        }

        [Fact]
        public void Create_WithNotNullParameters_ShouldNotThrowArgumentNullException()
        {
            // Arrange
            var configCreation = new Func<KickOpenTheDoorStep, CombatRoomStep>(s => new CombatRoomStep(s.CurrentPlayer, s.Card as MonsterCard));
            var configCondition = new Func<KickOpenTheDoorStep, bool>(s => s.Card is MonsterCard);

            // Act
            var exception = Record.Exception(() => Transition.Create(configCreation, configCondition));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void Create_WithNotNullParameters_ShouldReturnTransitionInstance()
        {
            // Arrange
            var configCreation = new Func<KickOpenTheDoorStep, CombatRoomStep>(s => new CombatRoomStep(s.CurrentPlayer, s.Card as MonsterCard));
            var configCondition = new Func<KickOpenTheDoorStep, bool>(s => s.Card is MonsterCard);

            // Act
            var transition = Transition.Create(configCreation, configCondition);

            // Assert
            Assert.NotNull(transition);
            Assert.IsAssignableFrom<Transition>(transition);
        }

        [Fact]
        public void Execute_WithNullStep_ShouldThorwArgumentNullException()
        {
            // Arrange
            var configCreation = new Func<KickOpenTheDoorStep, CombatRoomStep>(s => new CombatRoomStep(s.CurrentPlayer, s.Card as MonsterCard));
            var configCondition = new Func<KickOpenTheDoorStep, bool>(s => s.Card is MonsterCard);

            // Act
            var transition = Transition.Create(configCreation, configCondition);

            // Assert
            Assert.Throws<ArgumentNullException>(() => transition.Execute(null));
        }

        [Fact]
        public void Execute_WithKickDownTheDoorStep_AndTruthfulCondition_ShouldReturnCombatStepInstance()
        {
            // Arrange
            var table = Table.Empty();
            var player = new Player("Johny Cash", EGender.Male);
            var monsterCard = new BandOf3872Orcs();
            var step1 = new KickOpenTheDoorStep(player);

            var configCreation = new Func<KickOpenTheDoorStep, CombatRoomStep>(s => new CombatRoomStep(player, monsterCard));
            var configCondition = new Func<KickOpenTheDoorStep, bool>(s => true);

            // Act
            var transition = Transition.Create(configCreation, configCondition);
            var step2 = transition.Execute(step1);

            // Assert
            Assert.NotNull(step2);
            Assert.IsAssignableFrom<CombatRoomStep>(step2);
        }

        [Fact]
        public void Execute_WithKickDownTheDoorStep_AndFalseCondition_ShouldReturnNull()
        {
            // Arrange
            var table = Table.Empty();
            var player = new Player("Johny Cash", EGender.Male);
            var monsterCard = new BandOf3872Orcs();
            var step1 = new KickOpenTheDoorStep(player);

            var configCreation = new Func<KickOpenTheDoorStep, CombatRoomStep>(s => new CombatRoomStep(player, monsterCard));
            var configCondition = new Func<KickOpenTheDoorStep, bool>(s => false);

            // Act
            var transition = Transition.Create(configCreation, configCondition);
            var step2 = transition.Execute(step1);

            // Assert
            Assert.Null(step2);
        }
    }
}

using FluentAssertions;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards.Doors.Monsters;
using Munchkin.Core.Model.Cards.Treasures.OneShot;
using Munchkin.Core.Model.Phases;
using Munchkin.Core.Model.Phases.Events;
using Xunit;

namespace Munchkin.Core.Tests.Model.Phases
{
    public class CombatStatsTests
    {
        [Fact]
        public void From__OnTable_WithOneMonster_And_NoHelp__ShouldHave_PlayerStrengthEqual3_And_MonsterStrengthEqual1()
        {
            // Arrange
            const string PlayerFrankSinatraNickname = "frank.sinatra";
            const string PlayerJohnyCashNickname = "johny.cash";
            const string PlayerElonMuskNickname = "elon.musk";

            var table = Table.Empty().WithWinningLevel(10);
            var monster1 = new PottedPlant();
            var treasure1 = new ThousandGoldPieces();
            var treasure2 = new MutilateTheBodies();
            var player1 = new Player(PlayerFrankSinatraNickname, EGender.Male);
            var player2 = new Player(PlayerJohnyCashNickname, EGender.Male);
            var player3 = new Player(PlayerElonMuskNickname, EGender.Male);

            // Act
            table = table.Join(player1).Table;
            table = table.Join(player2).Table;
            table = table.Join(player3).Table;

            player1.TakeInHand(treasure1);
            player1.TakeInHand(treasure2);

            table = table.Play(treasure1);
            table = table.Play(treasure2);
            table = table.Play(monster1);

            var combatStats = CombatStats.From(table);

            // Assert
            combatStats.Should().NotBeNull();
            combatStats.Monsters.Should().NotBeNull();
            combatStats.Monsters.Should().HaveCount(1);
            combatStats.MonsterStrength.Should().Be(1);
            combatStats.FightingPlayer.Should().NotBeNull();
            combatStats.HelpingPlayer.Should().BeNull();
            combatStats.PlayersStrength.Should().Be(3);
        }

        [Fact]
        public void From__OnTable_WithOneMonster_And_HelpingHero__ShouldHave_PlayerStrengthEqual4_And_MonsterStrengthEqual1()
        {
            // Arrange
            const string PlayerFrankSinatraNickname = "frank.sinatra";
            const string PlayerJohnyCashNickname = "johny.cash";
            const string PlayerElonMuskNickname = "elon.musk";

            var table = Table.Empty().WithWinningLevel(10);
            var monster1 = new PottedPlant();
            var treasure1 = new ThousandGoldPieces();
            var treasure2 = new MutilateTheBodies();
            var player1 = new Player(PlayerJohnyCashNickname, EGender.Male);
            var player2 = new Player(PlayerFrankSinatraNickname, EGender.Male);
            var player3 = new Player(PlayerElonMuskNickname, EGender.Male);

            // Act
            table = table.Join(player1).Table;
            table = table.Join(player2).Table;
            table = table.Join(player3).Table;

            player1.TakeInHand(treasure1);
            player1.TakeInHand(treasure2);

            table.ActionLog.Add(new AskingForHelpPlayerEvent(PlayerFrankSinatraNickname));
            table.ActionLog.Add(new AskingForHelpAcceptedEvent(PlayerFrankSinatraNickname));

            table = table.Play(treasure1);
            table = table.Play(treasure2);
            table = table.Play(monster1);

            var combatStats = CombatStats.From(table);

            // Assert
            combatStats.Should().NotBeNull();
            combatStats.Monsters.Should().NotBeNull();
            combatStats.Monsters.Should().HaveCount(1);
            combatStats.MonsterStrength.Should().Be(1);
            combatStats.FightingPlayer.Should().NotBeNull();
            combatStats.HelpingPlayer.Should().NotBeNull();
            combatStats.PlayersStrength.Should().Be(4);
        }
    }
}

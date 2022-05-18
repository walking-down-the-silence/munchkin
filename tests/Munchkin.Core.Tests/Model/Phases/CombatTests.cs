using FluentAssertions;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Phases;
using Xunit;

namespace Munchkin.Core.Tests.Model.Phases
{
    public class CombatTests
    {
        [Fact]
        public void AskForHelp__OnTable_WithPlayer__ShouldHave_PlayerAskedNotNull()
        {
            // Arrange
            const string PlayerFrankSinatraNickname = "frank.sinatra";
            const string PlayerJohnyCashNickname = "johny.cash";
            const string PlayerElonMuskNickname = "elon.musk";

            var table = Table.Empty().WithWinningLevel(10);
            var player1 = new Player(PlayerFrankSinatraNickname, EGender.Male);
            var player2 = new Player(PlayerJohnyCashNickname, EGender.Male);
            var player3 = new Player(PlayerElonMuskNickname, EGender.Male);

            // Act
            table = table.Join(player1).Table;
            table = table.Join(player2).Table;
            table = table.Join(player3).Table;

            table = Combat.AskForHelp(table, player2);
            var help = AskingForHelp.From(table);

            // Assert
            help.Should().NotBeNull();
            help.FightingPlayer.Should().NotBeNull();
            help.FightingPlayer.Should().BeSameAs(player1);
            help.HelpingPlayer.Should().BeNull();
            help.PlayerAsked.Should().NotBeNull();
            help.PlayerAsked.Should().BeSameAs(player2);
            help.PlayersWhoRejected.Should().BeEmpty();
        }

        [Fact]
        public void AcceptHelpRequest__OnTable_WithPlayer__ShouldHave_HelpingPlayerNotNull()
        {
            // Arrange
            const string PlayerFrankSinatraNickname = "frank.sinatra";
            const string PlayerJohnyCashNickname = "johny.cash";
            const string PlayerElonMuskNickname = "elon.musk";

            var table = Table.Empty().WithWinningLevel(10);
            var player1 = new Player(PlayerFrankSinatraNickname, EGender.Male);
            var player2 = new Player(PlayerJohnyCashNickname, EGender.Male);
            var player3 = new Player(PlayerElonMuskNickname, EGender.Male);

            // Act
            table = table.Join(player1).Table;
            table = table.Join(player2).Table;
            table = table.Join(player3).Table;

            table = Combat.AskForHelp(table, player2);
            table = Combat.AcceptHelpRequest(table, player2);
            var help = AskingForHelp.From(table);

            // Assert
            help.Should().NotBeNull();
            help.FightingPlayer.Should().NotBeNull();
            help.FightingPlayer.Should().BeSameAs(player1);
            help.HelpingPlayer.Should().NotBeNull();
            help.HelpingPlayer.Should().BeSameAs(player2);
            help.PlayerAsked.Should().BeNull();
            help.PlayersWhoRejected.Should().BeEmpty();
        }

        [Fact]
        public void RejectHelpRequest__OnTable_WithPlayer__ShouldHave_HelpingPlayerEqualNull_And_RejectedPlayerNotEmpty()
        {
            // Arrange
            const string PlayerFrankSinatraNickname = "frank.sinatra";
            const string PlayerJohnyCashNickname = "johny.cash";
            const string PlayerElonMuskNickname = "elon.musk";

            var table = Table.Empty().WithWinningLevel(10);
            var player1 = new Player(PlayerFrankSinatraNickname, EGender.Male);
            var player2 = new Player(PlayerJohnyCashNickname, EGender.Male);
            var player3 = new Player(PlayerElonMuskNickname, EGender.Male);

            // Act
            table = table.Join(player1).Table;
            table = table.Join(player2).Table;
            table = table.Join(player3).Table;

            table = Combat.AskForHelp(table, player2);
            table = Combat.RejectHelpRequest(table, player2);
            var help = AskingForHelp.From(table);

            // Assert
            help.Should().NotBeNull();
            help.FightingPlayer.Should().NotBeNull();
            help.FightingPlayer.Should().BeSameAs(player1);
            help.HelpingPlayer.Should().BeNull();
            help.PlayerAsked.Should().BeNull();
            help.PlayersWhoRejected.Should().NotBeNullOrEmpty();
            help.PlayersWhoRejected.Should().Contain(player2);
        }
    }
}

using FluentAssertions;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Phases;
using Munchkin.Core.Model.Phases.Events;
using Xunit;

namespace Munchkin.Core.Tests.Model.Phases
{
    public class AskingForHelpTests
    {
        [Fact]
        public void From_TableWith3Players_ShouldHaveOneAcceptedAndOneRejected()
        {
            // Arrange
            var table = Table.Empty();
            var player1 = new Player("johny.cash", EGender.Male);
            var player2 = new Player("frank.sinatra", EGender.Male);
            var player3 = new Player("elon.musk", EGender.Male);

            // Act
            table = table.Join(player1).Table;
            table = table.Join(player2).Table;
            table = table.Join(player3).Table;

            var event1 = new AskingForHelpPlayerEvent(player2.Nickname);
            var event2 = new AskingForHelpRejectedEvent(player2.Nickname);
            var event3 = new AskingForHelpPlayerEvent(player3.Nickname);
            var event4 = new AskingForHelpAcceptedEvent(player3.Nickname);

            table.ActionLog.Add(event1);
            table.ActionLog.Add(event2);
            table.ActionLog.Add(event3);
            table.ActionLog.Add(event4);

            var askingForHelp = AskingForHelp.From(table);

            // Assert
            askingForHelp.Should().NotBeNull();
            askingForHelp.FightingPlayer.Should().NotBeNull();
            askingForHelp.FightingPlayer.Should().BeSameAs(player1);
            askingForHelp.HelpingPlayer.Should().NotBeNull();
            askingForHelp.HelpingPlayer.Should().BeSameAs(player3);
            askingForHelp.PlayersLeftToAsk.Should().NotBeNull();
            askingForHelp.PlayersLeftToAsk.Should().BeEmpty();
            askingForHelp.PlayersWhoRejected.Should().NotBeNull();
            askingForHelp.PlayersWhoRejected.Should().Contain(player2);
        }
    }
}

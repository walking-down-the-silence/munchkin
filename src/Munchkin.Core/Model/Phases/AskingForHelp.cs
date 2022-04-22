using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Attributes;
using System.Collections.Immutable;
using System.Linq;

namespace Munchkin.Core.Model.Phases
{
    public record AskingForHelp(
        Table Table,
        ImmutableList<Player> PlayersToAsk,
        Player HelpingPlayer,
        CombatRoom PreviousState
    )
    : StateBase<AskingForHelp>(Table, ImmutableList<Attribute>.Empty);

    public static class AskingForHelpExtensions
    {
        public static IState From(Table table, CombatRoom previousState)
        {
            var playersToAsk = ImmutableList.CreateRange(table.Players.Where(p => p != table.Players.Current));
            return new AskingForHelp(
                table,
                playersToAsk,
                null,
                previousState);
        }

        /// <summary>
        /// Sends a request for rhelp to a selected player.
        /// TODO: send a request for help to the target player
        /// </summary>
        /// <param name="state"></param>
        /// <param name="targetPlayer"></param>
        /// <returns></returns>
        public static IState Ask(this AskingForHelp state, Player targetPlayer) =>
            state with { PlayersToAsk = state.PlayersToAsk.Remove(targetPlayer) };

        /// <summary>
        /// Accepts request for help.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="helpingPlayer"></param>
        /// <returns></returns>
        public static IState Accept(this AskingForHelp state, Player helpingPlayer) =>
            state.PreviousState with { HelpingPlayer = helpingPlayer };

        /// <summary>
        /// Rejects rerquest for help.
        /// TODO: remove player that rejected from the list of players to ask
        /// </summary>
        /// <param name="state"></param>
        /// <param name="helpingPlayer"></param>
        /// <returns></returns>
        public static IState Reject(this AskingForHelp state, Player helpingPlayer) =>
            state.PreviousState with { HelpingPlayer = null };
    }
}

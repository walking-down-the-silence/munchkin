using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Phases
{
    public static class AskingForHelp
    {
        /// <summary>
        /// Sends a request for rhelp to a selected player.
        /// TODO: send a request for help to the target player
        /// </summary>
        /// <param name="state"></param>
        /// <param name="targetPlayer"></param>
        /// <returns></returns>
        public static IState Ask(this Help state, Player targetPlayer) =>
            state with { PlayersToAsk = state.PlayersToAsk.Remove(targetPlayer) };

        /// <summary>
        /// Accepts request for help.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="helpingPlayer"></param>
        /// <returns></returns>
        public static IState Accept(this Help state, Player helpingPlayer) =>
            state.PreviousState with { HelpingPlayer = helpingPlayer };

        /// <summary>
        /// Rejects rerquest for help.
        /// TODO: remove player that rejected from the list of players to ask
        /// </summary>
        /// <param name="state"></param>
        /// <param name="helpingPlayer"></param>
        /// <returns></returns>
        public static IState Reject(this Help state, Player helpingPlayer) =>
            state.PreviousState with { HelpingPlayer = null };
    }
}

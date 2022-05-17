using System.Collections.Immutable;

namespace Munchkin.Core.Model.Phases
{
    /// <summary>
    /// Defines the state when a fighting pllayer asks another player for help in combat.
    /// </summary>
    /// <param name="PlayersToAsk">A collection of remaining players to ask.</param>
    /// <param name="AskedPlayer">A player recently asked to help.</param>
    public record AskingForHelp(ImmutableList<Player> PlayersToAsk, Player AskedPlayer)
    {
        /// <summary>
        /// Sets the player who was recently asked to help.
        /// </summary>
        /// <param name="askedPlayer">The player asked.</param>
        /// <returns>Retuns a modified instance of the state.</returns>
        public AskingForHelp WithAskedPlayer(Player askedPlayer)
        {
            return this with
            {
                PlayersToAsk = PlayersToAsk.Remove(askedPlayer),
                AskedPlayer = askedPlayer
            };
        }

        /// <summary>
        /// Sets asked player to noone.
        /// </summary>
        /// <returns>Retuns a modified instance of the state.</returns>
        public AskingForHelp WithoutAskedPlayer()
        {
            return this with
            {
                AskedPlayer = null
            };
        }
    }
}

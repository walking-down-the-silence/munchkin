using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Expansions
{
    /// <summary>
    /// Defines the result of a player trying to join the playing table.
    /// </summary>
    /// <param name="Code">The code of the result.</param>
    /// <param name="Name">The human-readable message that describes the result.</param>
    public sealed record JoinTableResult(int Code, string Name) : Enumeration(Code, Name)
    {
        /// <summary>
        /// The player was able to join the playing table.
        /// </summary>
        public static JoinTableResult Joined => new(1, "Joined the table");

        /// <summary>
        /// The player left the playing table.
        /// </summary>
        public static JoinTableResult Left => new(2, "Left the table");

        /// <summary>
        /// The player was not able to join the table either because the game has started or ended.
        /// </summary>
        public static JoinTableResult Full => new(3, "Table is full");

        /// <summary>
        /// The playing table is empty with no players.
        /// </summary>
        public static JoinTableResult Empty => new(4, "Table is empty");

        /// <summary>
        /// The player who is trying to join the table does not seem to valid.
        /// </summary>
        public static JoinTableResult UserNotValid => new(5, "User not valid");
    }
}

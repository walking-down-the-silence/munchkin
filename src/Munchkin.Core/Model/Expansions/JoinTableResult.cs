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
        public static JoinTableResult JoinedRoom => new(1, "Joined The Room");

        /// <summary>
        /// The player left the playing table.
        /// </summary>
        public static JoinTableResult LeftRoom => new(2, "Left The Room");

        /// <summary>
        /// The player was nt able to join the table bacause it has no free spots.
        /// </summary>
        public static JoinTableResult RoomFull => new(3, "Room Is Full");

        /// <summary>
        /// The playing table is empty with no players.
        /// </summary>
        public static JoinTableResult RoomEmpty => new(4, "Room Is Empty");

        /// <summary>
        /// The player who is trying to join the table does not seem to valid.
        /// </summary>
        public static JoinTableResult InvalidUser => new(5, "Invalid User");
    }
}

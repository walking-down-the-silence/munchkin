using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Expansions
{
    /// <summary>
    /// Defines the result of player selecting an expansion to play with.
    /// </summary>
    /// <param name="Code">The code of the result.</param>
    /// <param name="Name">The human-readable message that describes the result.</param>
    public sealed record SelectExpansionResult(int Code, string Name) : Enumeration(Code, Name)
    {
        /// <summary>
        /// The player selected the expansion.
        /// </summary>
        public static SelectExpansionResult OptionSelected => new(1, "Option Selected");

        /// <summary>
        /// The player unselected the expansion.
        /// </summary>
        public static SelectExpansionResult OptionUnselected => new(2, "Option Unselected");

        /// <summary>
        /// The player used an invalid code or unexisting expansion.
        /// </summary>
        public static SelectExpansionResult InvalidOptionCode => new(3, "Invalid Option Code");
    }
}

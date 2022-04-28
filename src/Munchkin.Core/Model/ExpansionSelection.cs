namespace Munchkin.Core.Model.Expansions
{
    /// <summary>
    /// Defines game expansion option selection state.
    /// </summary>
    /// <param name="Code">The code of the expansion.</param>
    /// <param name="Title">The human-readable title of the expansion.</param>
    /// <param name="Selected">Gets if the expansion is selected or not.</param>
    public record ExpansionSelection(
        string Code,
        string Title,
        bool Selected);
}
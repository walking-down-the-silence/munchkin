namespace Munchkin.Core.Model.Expansions
{
    /// <summary>
    /// Defines the game expansion option.
    /// </summary>
    /// <param name="Code">The code of the expansion.</param>
    /// <param name="Title">The human-readable title of the expansion.</param>
    public sealed record ExpansionOption(
        string Code,
        string Title);
}
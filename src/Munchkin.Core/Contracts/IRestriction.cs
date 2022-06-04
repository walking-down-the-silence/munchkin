namespace Munchkin.Core.Contracts
{
    /// <summary>
    /// Defines the restriction to how the card can be used.
    /// </summary>
    public interface IRestriction
    {
        /// <summary>
        /// Get the human readable title of the attribute.
        /// </summary>
        string Title { get; }
    }
}

namespace Munchkin.Core.Contracts.Attributes
{
    /// <summary>
    /// Defines the attribute and how it can be used.
    /// </summary>
    public interface IAttribute
    {
        /// <summary>
        /// Get the human readable title of the attribute.
        /// </summary>
        string Title { get; }
    }
}

namespace Munchkin.Core.Contracts.Attributes
{
    /// <summary>
    /// Defines the ability and it's description which can be used.
    /// Produces an action instance that can be executed.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public interface IAttribute
    {
        string Title { get; }

        string Description { get; }
    }
}

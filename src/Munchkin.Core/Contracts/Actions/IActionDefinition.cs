namespace Munchkin.Core.Contracts.Actions
{
    /// <summary>
    /// Defines a container for the actual action and it's description.
    /// </summary>
    /// <typeparam name="TState">The state of the game.</typeparam>
    public interface IActionDefinition<TState>
    {
        /// <summary>
        /// A factory method to create the actual action.
        /// </summary>
        /// <returns>An instance of the actual action.</returns>
        IAction<TState> Create();
    }
}

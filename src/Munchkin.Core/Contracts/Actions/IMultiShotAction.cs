namespace Munchkin.Core.Contracts
{
    /// <summary>
    /// Defines an action that can be executed multiple times.
    /// </summary>
    /// <typeparam name="TState"> The context to use for execution. </typeparam>
    public interface IMultiShotAction<TState> : IAction<TState>
    {
        /// <summary>
        /// Gets the total amount of executions for this action.
        /// </summary>
        int ExecutionsCount { get; }

        /// <summary>
        /// Gets the amount of executions left for this action.
        /// </summary>
        int ExecutionsLeft { get; }
    }
}

namespace Munchkin.Core.Contracts.Actions
{
    /// <summary>
    /// Defines an action that can be executed multiple times, but need to be reset before.
    /// </summary>
    /// <typeparam name="TState"> The context to use for execution. </typeparam>
    public interface IRenewableAction<TState> : IAction<TState>
    {
        /// <summary>
        /// Resets the action for further executions.
        /// </summary>
        /// <param name="state"> The context instance used to reset the action. </param>
        /// <returns> Returns if reset was successful. </returns>
        bool Reset(TState state);
    }
}

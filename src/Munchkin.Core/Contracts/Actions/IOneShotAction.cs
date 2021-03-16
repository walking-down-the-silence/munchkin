namespace Munchkin.Core.Contracts
{
    /// <summary>
    /// Defines an action that can be executed single time.
    /// </summary>
    /// <typeparam name="TState"> The context to use for execution. </typeparam>
    public interface IOneShotAction<TState> : IAction<TState>
    {
    }
}

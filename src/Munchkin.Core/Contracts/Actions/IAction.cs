using System.Threading.Tasks;

namespace Munchkin.Core.Contracts
{
    /// <summary>
    /// Defines an action that can be executed and modifies the state.
    /// </summary>
    /// <typeparam name="TState"> The context to use for execution. </typeparam>
    public interface IAction<TState>
    {
        /// <summary>
        /// The title for the action.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// A short description ofthe action.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Checks if the action can be applied under provided state.
        /// </summary>
        /// <param name="state">Original state instance.</param>
        /// <returns>Return if action can be executed.</returns>
        bool CanExecute(TState state);

        /// <summary>
        /// Executes the action awhichmodifies the state.
        /// </summary>
        /// <param name="state">Original state instance.</param>
        /// <returns>A modified state instance.</returns>
        Task<TState> ExecuteAsync(TState state);
    }
}

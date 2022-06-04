namespace Munchkin.Core.Contracts
{
    /// <summary>
    /// An effect that changes the state when applied.
    /// Works as a "push" model and can be used and an action.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public interface IEffect<TState>
    {
        /// <summary>
        /// Applies the ffects and modifies the provided state.
        /// </summary>
        /// <param name="state">Original state instance.</param>
        /// <returns>A modified state instance.</returns>
        TState Apply(TState state);
    }
}

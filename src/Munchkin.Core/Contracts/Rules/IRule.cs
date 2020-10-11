namespace Munchkin.Core.Contracts
{
    /// <summary>
    /// A rule that encapsulates the logic to check if an action can be executed.
    /// Works as a "pull" model and can be used to calculate state, but does not change it.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public interface IRule<in TState>
    {
        bool Satisfies(TState state);
    }
}

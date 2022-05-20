namespace Munchkin.Core.Contracts.Rules
{
    /// <summary>
    /// Encapsulates the logic to check if the game state satisfies the rule
    /// </summary>
    /// <typeparam name="TState">The state to check the rule against.</typeparam>
    public interface IRule<in TState>
    {
        bool Satisfies(TState state);
    }
}

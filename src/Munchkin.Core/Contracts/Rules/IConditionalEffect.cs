namespace Munchkin.Core.Contracts
{
    public interface IConditionalEffect<TState> : IEffect<TState>, IRule<TState>
    {
    }
}

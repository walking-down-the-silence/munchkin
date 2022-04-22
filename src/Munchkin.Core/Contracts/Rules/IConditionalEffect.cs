using Munchkin.Core.Contracts.Actions;

namespace Munchkin.Core.Contracts.Rules
{
    public interface IConditionalEffect<TState> : IEffect<TState>, IRule<TState>
    {
    }
}

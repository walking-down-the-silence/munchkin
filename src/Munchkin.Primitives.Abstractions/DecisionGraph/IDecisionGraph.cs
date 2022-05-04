namespace Munchkin.Core.Primitives
{
    public interface IDecisionGraph<TState>
    {
        Task<TState> Resolve(TState state, IStep<TState> step);
    }
}

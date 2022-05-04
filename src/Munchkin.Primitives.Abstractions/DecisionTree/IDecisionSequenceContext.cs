namespace Munchkin.Core.Primitives
{
    public interface IDecisionSequenceContext<TState>
    {
        IDecisionTreeContext<TState> Then(IStep<TState> step);
    }
}

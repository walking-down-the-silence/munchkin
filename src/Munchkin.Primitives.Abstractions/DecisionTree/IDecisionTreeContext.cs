namespace Munchkin.Core.Primitives
{
    public interface IDecisionTreeContext<TState> :
        IDecisionConditionContext<TState>,
        IDecisionSequenceContext<TState>,
        IDecisionTreeBuilder<TState>
    {
    }
}

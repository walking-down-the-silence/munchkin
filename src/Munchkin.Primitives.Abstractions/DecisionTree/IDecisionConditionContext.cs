namespace Munchkin.Core.Primitives
{
    public interface IDecisionConditionContext<TState>
    {
        IDecisionTreeBuilder<TState> Condition(
            Func<TState, Task<bool>> condition,
            Func<IDecisionTreeContext<TState>, IDecisionTreeBuilder<TState>> branch1,
            Func<IDecisionTreeContext<TState>, IDecisionTreeBuilder<TState>> branch2);
    }
}

namespace Munchkin.Core.Primitives
{
    public interface IDecisionTreeContext :
        IDecisionConditionContext,
        IDecisionSequenceContext,
        IDecisionTreeBuilder
    {
    }
}

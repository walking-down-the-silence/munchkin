namespace Munchkin.Core.Contracts.Stages
{
    public interface IDecisionTreeContext :
        IDecisionConditionContext,
        IDecisionSequenceContext,
        IDecisionTreeBuilder
    {
    }
}

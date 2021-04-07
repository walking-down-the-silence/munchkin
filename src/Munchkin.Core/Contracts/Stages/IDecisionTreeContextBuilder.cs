using System;

namespace Munchkin.Core.Contracts.Stages
{
    public interface IDecisionTreeContextBuilder :
        ITransitionGraphBuilder,
        IDecisionConditionBuilder,
        IDecisionSequenceBuilder,
        IDecisionTreeBuilder
    {
    }

    public interface ITransitionGraphBuilder : IDecisionTreeBuilder
    {
        ITransitionGraphBuilder Transition(Action<ITransitionBuilder> transiftionConfig);
    }

    public interface ITransitionBuilder
    {
        ITransitionToBuilder<TSource> From<TSource>();
    }

    public interface ITransitionToBuilder<TSource>
    {
        ITransitionToBuilder<TSource> To<TResult>(Func<TSource, TResult> configCreation, Func<TSource, bool> configCondition);
    }
}

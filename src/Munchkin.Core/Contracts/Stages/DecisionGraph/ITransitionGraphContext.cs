using System;

namespace Munchkin.Core.Contracts.Stages
{
    public interface ITransitionGraphContext :
        IDecisionGraphBuilder
    {
        ITransitionGraphContext Transition(Action<ITransitionFromContext> transiftionConfig);
    }
}

using System;

namespace Munchkin.Core.Primitives
{
    public interface ITransitionGraphContext :
        IDecisionGraphBuilder
    {
        ITransitionGraphContext Transition(Action<ITransitionFromContext> transiftionConfig);
    }
}

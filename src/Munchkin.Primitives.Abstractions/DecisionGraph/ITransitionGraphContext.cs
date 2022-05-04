using System;

namespace Munchkin.Core.Primitives
{
    public interface ITransitionGraphContext<TState> :
        IDecisionGraphBuilder<TState>
    {
        ITransitionGraphContext<TState> Transition(
            Action<ITransitionFromContext<TState>> transiftionConfig);
    }
}

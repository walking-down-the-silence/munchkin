namespace Munchkin.Core.Primitives
{
    public class DecisionGraph<TState> :
        IDecisionGraph<TState>
    {
        private readonly TransitionRegister<TState> _transitionRegister;

        public DecisionGraph(TransitionRegister<TState> transitionRegister)
        {
            _transitionRegister = transitionRegister ?? throw new ArgumentNullException(nameof(transitionRegister));
        }

        public static ITransitionGraphContext<TState> Empty()
        {
            return new DecisionGraphBuilder<TState>();
        }

        public async Task<TState> Resolve(TState state, IStep<TState> step)
        {
            var currentStep = step;

            while (currentStep != null)
            {
                state = await currentStep.Resolve(state);
                currentStep = _transitionRegister.TransitionFrom(currentStep);
            }

            return state;
        }
    }
}

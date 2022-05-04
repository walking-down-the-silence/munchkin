namespace Munchkin.Core.Primitives
{
    internal class DecisionGraphBuilder<T> :
        ITransitionGraphContext<T>
    {
        private readonly TransitionRegister<T> _transitionRegister;

        public DecisionGraphBuilder()
        {
            _transitionRegister = new TransitionRegister<T>();
        }

        public DecisionGraphBuilder(TransitionRegister<T> transitionRegister)
        {
            _transitionRegister = transitionRegister ?? throw new ArgumentNullException(nameof(transitionRegister));
        }

        public IDecisionGraph<T> Build()
        {
            return new DecisionGraph<T>(_transitionRegister);
        }

        public ITransitionGraphContext<T> Transition(Action<ITransitionFromContext<T>> transiftionConfig)
        {
            if (transiftionConfig is null)
                throw new ArgumentNullException(nameof(transiftionConfig));

            var transitionBuilder = new TransitionFromBuilder<T>(_transitionRegister);
            transiftionConfig.Invoke(transitionBuilder);
            return new DecisionGraphBuilder<T>(_transitionRegister);
        }
    }
}

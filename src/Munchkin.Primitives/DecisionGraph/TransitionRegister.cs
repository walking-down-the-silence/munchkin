namespace Munchkin.Core.Primitives
{
    public class TransitionRegister<T> :
        ITransitionRegister<T>,
        ITransitionProvider<T>
    {
        private readonly Dictionary<string, TransitionHandler<T>> _transitions = new();

        public void Register<TSource>(string fromStepName, ITransition<T> transition)
            where TSource : IStep<T>
        {
            if (transition is null)
                throw new ArgumentNullException(nameof(transition));

            var handler = _transitions.ContainsKey(fromStepName)
                ? new TransitionHandler<T>(transition, _transitions[fromStepName])
                : new TransitionHandler<T>(transition, null);

            _transitions[fromStepName] = handler;
        }

        public IStep<T> TransitionFrom(IStep<T> currentStep)
        {
            if (currentStep is null)
                throw new ArgumentNullException(nameof(currentStep));

            var handler = _transitions.ContainsKey(currentStep.Name)
                ? _transitions[currentStep.Name]
                : default;

            return handler?.Handle(currentStep);
        }
    }
}

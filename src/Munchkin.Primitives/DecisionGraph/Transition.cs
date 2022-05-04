namespace Munchkin.Core.Primitives
{
    public static class Transition
    {
        public static Transition<TState> Create<TState, TSource, TTarget>(
            Func<TSource, TTarget> configCreation,
            Func<TSource, bool> configCondition)
            where TSource : IStep<TState>
            where TTarget : IStep<TState>
        {
            if (configCreation is null)
                throw new ArgumentNullException(nameof(configCreation));

            if (configCondition is null)
                throw new ArgumentNullException(nameof(configCondition));

            Func<IStep<TState>, IStep<TState>> creator = (s) => configCreation.Invoke((TSource)s);
            Func<IStep<TState>, bool> condition = (s) => configCondition.Invoke((TSource)s);
            return new Transition<TState>(nameof(TSource), nameof(TTarget), creator, condition);
        }
    }

    public class Transition<TState> : ITransition<TState>
    {
        private readonly string _sourceStepName;
        private readonly string _targetStepName;
        private readonly Func<IStep<TState>, IStep<TState>> _creator;
        private readonly Func<IStep<TState>, bool> _condition;

        public Transition(
            string sourceStepName,
            string targetStepName,
            Func<IStep<TState>, IStep<TState>> creator,
            Func<IStep<TState>, bool> condition)
        {
            if (string.IsNullOrEmpty(sourceStepName))
                throw new ArgumentException($"'{nameof(sourceStepName)}' cannot be null or empty.", nameof(sourceStepName));

            if (string.IsNullOrEmpty(targetStepName))
                throw new ArgumentException($"'{nameof(targetStepName)}' cannot be null or empty.", nameof(targetStepName));

            _sourceStepName = sourceStepName;
            _targetStepName = targetStepName;
            _creator = creator ?? throw new ArgumentNullException(nameof(creator));
            _condition = condition ?? throw new ArgumentNullException(nameof(condition));
        }

        public bool CanExecute(IStep<TState> currentStep)
        {
            if (currentStep is null)
                throw new ArgumentNullException(nameof(currentStep));

            return _condition.Invoke(currentStep);
        }

        public IStep<TState> Execute(IStep<TState> currentStep)
        {
            if (currentStep is null)
                throw new ArgumentNullException(nameof(currentStep));

            var result = _condition.Invoke(currentStep)
                ? _creator.Invoke(currentStep)
                : default;

            return result;
        }
    }
}

namespace Munchkin.Core.Primitives
{
    internal class TransitionToBuilder<T, TSource> :
        ITransitionToContext<T, TSource>
        where TSource : IStep<T>
    {
        private readonly string _stepName;
        private readonly ITransitionRegister<T> _transitionRegister;

        public TransitionToBuilder(string stepName, ITransitionRegister<T> transitionRegister)
        {
            _stepName = stepName ?? throw new ArgumentNullException(nameof(stepName));
            _transitionRegister = transitionRegister ?? throw new ArgumentNullException(nameof(transitionRegister));
        }

        public ITransitionToContext<T, TSource> To<TResult>(
            Func<TSource, TResult> configCreation,
            Func<TSource, bool> configCondition)
            where TResult : IStep<T>
        {
            return InternalTo(configCreation, configCondition);
        }

        public ITransitionToContext<T, TSource> To<TResult>(
            Func<TSource, TResult> configCreation)
            where TResult : IStep<T>
        {
            return InternalTo(configCreation, s => true);
        }

        private ITransitionToContext<T, TSource> InternalTo<TResult>(
            Func<TSource, TResult> configCreation,
            Func<TSource, bool> configCondition)
            where TResult : IStep<T>
        {
            var transition = Transition.Create<T, TSource, TResult>(configCreation, configCondition);
            _transitionRegister.Register<TSource>(_stepName, transition);
            return new TransitionToBuilder<T, TSource>(_stepName, _transitionRegister);
        }
    }
}

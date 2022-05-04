namespace Munchkin.Core.Primitives
{
    internal class TransitionFromBuilder<T> :
        ITransitionFromContext<T>
    {
        private readonly ITransitionRegister<T> _transitionRegister;

        public TransitionFromBuilder(ITransitionRegister<T> transitionRegister)
        {
            _transitionRegister = transitionRegister ?? throw new ArgumentNullException(nameof(transitionRegister));
        }

        public ITransitionToContext<T, TSource> From<TSource>(string stepName)
            where TSource : IStep<T>
        {
            return new TransitionToBuilder<T, TSource>(stepName, _transitionRegister);
        }
    }
}

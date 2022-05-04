namespace Munchkin.Core.Primitives
{
    internal class TransitionHandler<T>
    {
        private readonly ITransition<T> _transition;
        private readonly TransitionHandler<T> _nextHandler;

        public TransitionHandler(ITransition<T> current, TransitionHandler<T> next)
        {
            _transition = current ?? throw new ArgumentNullException(nameof(current));
            _nextHandler = next;
        }

        public IStep<T> Handle(IStep<T> step) =>
            _transition.CanExecute(step)
                ? _transition.Execute(step)
                : _nextHandler?.Handle(step);
    }
}

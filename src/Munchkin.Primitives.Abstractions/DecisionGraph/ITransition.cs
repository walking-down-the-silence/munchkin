namespace Munchkin.Core.Primitives
{
    public interface ITransition<TState>
    {
        bool CanExecute(IStep<TState> currentStep);

        IStep<TState> Execute(IStep<TState> currentStep);
    }
}

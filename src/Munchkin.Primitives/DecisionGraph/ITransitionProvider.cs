namespace Munchkin.Core.Primitives
{
    public interface ITransitionProvider<T>
    {
        IStep<T> TransitionFrom(IStep<T> currentStep);
    }
}

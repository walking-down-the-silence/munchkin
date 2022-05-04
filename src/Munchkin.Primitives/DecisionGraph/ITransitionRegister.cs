namespace Munchkin.Core.Primitives
{
    public interface ITransitionRegister<T>
    {
        void Register<TSource>(string fromStepName, ITransition<T> transition)
             where TSource : IStep<T>;
    }
}

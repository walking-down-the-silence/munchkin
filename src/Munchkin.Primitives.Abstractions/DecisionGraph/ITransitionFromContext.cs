namespace Munchkin.Core.Primitives
{
    public interface ITransitionFromContext<TState>
    {
        ITransitionToContext<TState, TSource> From<TSource>(string stepName)
            where TSource : IStep<TState>;
    }
}

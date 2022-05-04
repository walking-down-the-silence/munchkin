namespace Munchkin.Core.Primitives
{
    public interface ITransitionToContext<TState, TSource>
        where TSource : IStep<TState>
    {
        ITransitionToContext<TState, TSource> To<TTarget>(
            Func<TSource, TTarget> configCreation,
            Func<TSource, bool> configCondition)
            where TTarget : IStep<TState>;
        ITransitionToContext<TState, TSource> To<TTarget>(
            Func<TSource, TTarget> configCreation)
            where TTarget : IStep<TState>;
    }
}

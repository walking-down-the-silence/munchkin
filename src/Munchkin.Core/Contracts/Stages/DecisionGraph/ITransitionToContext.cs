using Munchkin.Core.Model;
using System;

namespace Munchkin.Core.Contracts.Stages
{
    public interface ITransitionToContext<TSource>
        where TSource : IStep<Table>
    {
        ITransitionToContext<TSource> To<TResult>(
            Func<TSource, TResult> configCreation,
            Func<TSource, bool> configCondition)
            where TResult : IStep<Table>;
        ITransitionToContext<TSource> To<TResult>(
            Func<TSource, TResult> configCreation)
            where TResult : IStep<Table>;
    }
}

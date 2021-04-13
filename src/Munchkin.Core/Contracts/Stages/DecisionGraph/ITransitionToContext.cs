using Munchkin.Core.Model;
using System;

namespace Munchkin.Core.Contracts.Stages
{
    public interface ITransitionToContext<TSource>
        where TSource : IStep<Table>
    {
        ITransitionToContext<TSource> To<TTarget>(
            Func<TSource, TTarget> configCreation,
            Func<TSource, bool> configCondition)
            where TTarget : IStep<Table>;
        ITransitionToContext<TSource> To<TTarget>(
            Func<TSource, TTarget> configCreation)
            where TTarget : IStep<Table>;
    }
}

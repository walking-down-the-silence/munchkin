using Munchkin.Core.Model;

namespace Munchkin.Core.Contracts.Stages
{
    public interface ITransitionFromContext
    {
        ITransitionToContext<TSource> From<TSource>(string stepName)
            where TSource : IStep<Table>;
    }
}

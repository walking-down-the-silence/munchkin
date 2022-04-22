using Munchkin.Core.Model;

namespace Munchkin.Core.Primitives
{
    public interface ITransitionFromContext
    {
        ITransitionToContext<TSource> From<TSource>(string stepName)
            where TSource : IStep<Table>;
    }
}

using System.Threading.Tasks;

namespace Munchkin.Core.Primitives
{
    public interface IStep<TContext>
    {
        string Name { get; }

        Task<TContext> Resolve(TContext context);
    }
}

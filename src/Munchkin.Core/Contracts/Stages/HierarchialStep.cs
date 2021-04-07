using System.Threading.Tasks;

namespace Munchkin.Core.Contracts
{
    public abstract class HierarchialStep<TContext> : IStep<TContext>
    {
        public abstract Task<TContext> Resolve(TContext context);
    }
}

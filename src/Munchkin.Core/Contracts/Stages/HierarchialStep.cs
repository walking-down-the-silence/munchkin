using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Stages
{
    public abstract class HierarchialStep<TContext> : IStep<TContext>
    {
        public string Name => throw new System.NotImplementedException();

        public abstract Task<TContext> Resolve(TContext context);
    }
}

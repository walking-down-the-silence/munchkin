using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Stages
{
    public abstract class TerminalStep<TContext> : IStep<TContext>
    {
        public abstract Task<TContext> Resolve(TContext context);
    }
}

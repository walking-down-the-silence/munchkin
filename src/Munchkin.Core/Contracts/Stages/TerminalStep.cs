using System.Threading.Tasks;

namespace Munchkin.Core.Contracts
{
    public abstract class TerminalStep<TContext> : IStep<TContext>
    {
        public abstract Task<TContext> Resolve(TContext context);
    }
}

using System.Threading.Tasks;

namespace Munchkin.Core.Contracts
{
    public abstract class StepBase<TContext> : IStep<TContext>
    {
        public async Task<TContext> Resolve(TContext context)
        {
            context = await OnBeforeResolve(context);
            context = await OnResolve(context);
            context = await OnAfterResolve(context);
            return context;
        }

        protected abstract Task<TContext> OnResolve(TContext context);

        protected Task<TContext> OnBeforeResolve(TContext context)
        {
            return Task.FromResult(context);
        }

        protected Task<TContext> OnAfterResolve(TContext context)
        {
            return Task.FromResult(context);
        }
    }
}

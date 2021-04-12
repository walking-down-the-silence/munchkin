using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Stages
{
    public abstract class StepBase<TContext> : IStep<TContext>
    {
        protected StepBase(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new System.ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            Name = name;
        }

        public string Name { get; }

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

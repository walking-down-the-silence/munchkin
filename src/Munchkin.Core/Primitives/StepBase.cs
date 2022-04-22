using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Primitives
{
    public abstract class StepBase<TContext> : IStep<TContext>
    {
        protected StepBase(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));

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

        protected virtual Task<TContext> OnResolve(TContext context) => Task.FromResult(context);

        protected virtual Task<TContext> OnBeforeResolve(TContext context) => Task.FromResult(context);

        protected virtual Task<TContext> OnAfterResolve(TContext context) => Task.FromResult(context);
    }
}

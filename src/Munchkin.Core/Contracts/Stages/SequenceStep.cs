using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts
{
    public abstract class SequenceStep<TContext> : IStep<TContext>
    {
        private readonly List<IStep<TContext>> _steps = new();

        public virtual async Task<TContext> Resolve(TContext context)
        {
            foreach (var step in _steps)
            {
                context = await step.Resolve(context);
            }

            return context;
        }

        protected void AddStep(IStep<TContext> step) => _steps.Add(step);
    }
}

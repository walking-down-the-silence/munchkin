using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Stages
{
    public abstract class SequenceStep<TContext> : IStep<TContext>
    {
        private readonly List<IStep<TContext>> _steps = new();

        public string Name => throw new System.NotImplementedException();

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

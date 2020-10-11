using System;

namespace Munchkin.Console
{
    public class ExecutionExpressionFlow<TContext> : IFlowContext<TContext>
    {
        private readonly Func<TContext, TContext> _action;

        public ExecutionExpressionFlow(Func<TContext, TContext> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public Func<TContext, TContext> Build()
        {
            return _action;
        }
    }
}

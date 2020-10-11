using System;

namespace Munchkin.Console
{
    public class LoopExpressionFlow<TContext> : IFlowContext<TContext>
    {
        private readonly IFlowContext<TContext> _parentFlow;
        private readonly Func<TContext, bool> _condition;
        private readonly Func<IFlowContext<TContext>, IFlowContext<TContext>> _actionFlow;

        public LoopExpressionFlow(
            Func<TContext, bool> condition,
            Func<IFlowContext<TContext>, IFlowContext<TContext>> actionFlow)
        {
            _condition = condition ?? throw new ArgumentNullException(nameof(condition));
            _actionFlow = actionFlow ?? throw new ArgumentNullException(nameof(actionFlow));
        }

        public LoopExpressionFlow(
            IFlowContext<TContext> parentFlow,
            Func<TContext, bool> condition,
            Func<IFlowContext<TContext>, IFlowContext<TContext>> actionFlow)
        {
            _parentFlow = parentFlow ?? throw new ArgumentNullException(nameof(parentFlow));
            _condition = condition ?? throw new ArgumentNullException(nameof(condition));
            _actionFlow = actionFlow ?? throw new ArgumentNullException(nameof(actionFlow));
        }

        public Func<TContext, TContext> Build()
        {
            return new Func<TContext, TContext>(state =>
            {
                // TODO: check if first comes condition or execution
                TContext nextState = _parentFlow == null ? state : _parentFlow.Build().Invoke(state);
                Func<TContext, TContext> action = _actionFlow.Invoke(this).Build();

                while (_condition.Invoke(nextState))
                {
                    nextState = action.Invoke(nextState);
                }

                return nextState;
            });
        }
    }
}

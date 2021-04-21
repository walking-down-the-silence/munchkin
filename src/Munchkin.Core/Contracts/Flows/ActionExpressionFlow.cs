using System;

namespace Munchkin.Core.Contracts.Flows
{
    public class ActionExpressionFlow<TContext> : IFlowContext<TContext>
    {
        private readonly IFlowContext<TContext> _parentFlow;
        private readonly Func<IFlowContext<TContext>, IFlowContext<TContext>> _action;

        public ActionExpressionFlow(
            Func<IFlowContext<TContext>, IFlowContext<TContext>> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public ActionExpressionFlow(
            IFlowContext<TContext> parentFlow,
            Func<IFlowContext<TContext>, IFlowContext<TContext>> action)
        {
            _parentFlow = parentFlow ?? throw new ArgumentNullException(nameof(parentFlow));
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public Func<TContext, TContext> Build()
        {
            return new Func<TContext, TContext>(state =>
            {
                TContext nextState = _parentFlow == null ? state : _parentFlow.Build().Invoke(state);
                return _action.Invoke(this).Build().Invoke(nextState);
            });
        }
    }
}

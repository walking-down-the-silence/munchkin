using System;

namespace Munchkin.Core.Contracts.Flows
{
    public class ConditionExpressionFlow<TContext> : IFlowContext<TContext>
    {
        private readonly IFlowContext<TContext> _parentFlow;
        private readonly Func<TContext, bool> _condition;
        private readonly Func<IFlowContext<TContext>, IFlowContext<TContext>> _positiveFlow;
        private readonly Func<IFlowContext<TContext>, IFlowContext<TContext>> _negativeFlow;

        public ConditionExpressionFlow(
            Func<TContext, bool> condition,
            Func<IFlowContext<TContext>, IFlowContext<TContext>> positiveFlow,
            Func<IFlowContext<TContext>, IFlowContext<TContext>> negativeFlow)
        {
            _condition = condition ?? throw new ArgumentNullException(nameof(condition));
            _positiveFlow = positiveFlow ?? throw new ArgumentNullException(nameof(positiveFlow));
            _negativeFlow = negativeFlow ?? throw new ArgumentNullException(nameof(negativeFlow));
        }

        public ConditionExpressionFlow(
            IFlowContext<TContext> parentFlow,
            Func<TContext, bool> condition,
            Func<IFlowContext<TContext>, IFlowContext<TContext>> positiveFlow,
            Func<IFlowContext<TContext>, IFlowContext<TContext>> negativeFlow)
        {
            _parentFlow = parentFlow ?? throw new ArgumentNullException(nameof(parentFlow));
            _condition = condition ?? throw new ArgumentNullException(nameof(condition));
            _positiveFlow = positiveFlow ?? throw new ArgumentNullException(nameof(positiveFlow));
            _negativeFlow = negativeFlow ?? throw new ArgumentNullException(nameof(negativeFlow));
        }

        public Func<TContext, TContext> Build()
        {
            return new Func<TContext, TContext>(state =>
            {
                TContext nextState = _parentFlow == null ? state : _parentFlow.Build().Invoke(state);
                return _condition.Invoke(nextState)
                    ? _positiveFlow.Invoke(this).Build().Invoke(nextState)
                    : _negativeFlow.Invoke(this).Build().Invoke(nextState);
            });
        }
    }
}

using System;

namespace Munchkin.Core.Contracts.Flows
{
    public static class Flow
    {
        public static IFlowContext<TContext> Action<TContext>(
            Func<IFlowContext<TContext>, IFlowContext<TContext>> action)
        {
            return new ActionExpressionFlow<TContext>(action);
        }

        public static IFlowContext<TContext> Action<TContext>(
            this IFlowContext<TContext> context,
            Func<IFlowContext<TContext>, IFlowContext<TContext>> action)
        {
            return new ActionExpressionFlow<TContext>(context, action);
        }

        public static IFlowContext<TContext> Loop<TContext>(
            Func<TContext, bool> condition,
            Func<IFlowContext<TContext>, IFlowContext<TContext>> action)
        {
            return new LoopExpressionFlow<TContext>(condition, action);
        }

        public static IFlowContext<TContext> Loop<TContext>(
            this IFlowContext<TContext> context,
            Func<TContext, bool> condition,
            Func<IFlowContext<TContext>, IFlowContext<TContext>> action)
        {
            return new LoopExpressionFlow<TContext>(context, condition, action);
        }

        public static IFlowContext<TContext> Condition<TContext>(
            Func<TContext, bool> condition,
            Func<IFlowContext<TContext>, IFlowContext<TContext>> positive,
            Func<IFlowContext<TContext>, IFlowContext<TContext>> negative)
        {
            return new ConditionExpressionFlow<TContext>(condition, positive, negative);
        }

        public static IFlowContext<TContext> Condition<TContext>(
            this IFlowContext<TContext> context,
            Func<TContext, bool> condition,
            Func<IFlowContext<TContext>, IFlowContext<TContext>> positive,
            Func<IFlowContext<TContext>, IFlowContext<TContext>> negative)
        {
            return new ConditionExpressionFlow<TContext>(context, condition, positive, negative);
        }

        public static IFlowContext<TContext> Execute<TContext>(
            Func<TContext, TContext> action)
        {
            return new ExecutionExpressionFlow<TContext>(action);
        }

        public static IFlowContext<TContext> Execute<TContext>(
            this IFlowContext<TContext> context,
            Func<TContext, TContext> action)
        {
            return new ExecutionExpressionFlow<TContext>(action);
        }
    }
}

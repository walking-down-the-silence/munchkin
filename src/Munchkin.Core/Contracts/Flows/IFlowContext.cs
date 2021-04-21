using System;

namespace Munchkin.Core.Contracts.Flows
{
    public interface IFlowContext<TContext>
    {
        Func<TContext, TContext> Build();
    }
}

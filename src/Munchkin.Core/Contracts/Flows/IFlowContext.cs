using System;

namespace Munchkin.Console
{
    public interface IFlowContext<TContext>
    {
        Func<TContext, TContext> Build();
    }
}

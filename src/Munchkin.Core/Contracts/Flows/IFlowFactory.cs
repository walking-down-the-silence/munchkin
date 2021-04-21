namespace Munchkin.Core.Contracts.Flows
{
    public interface IFlowFactory<TContext>
    {
        IFlowContext<TContext> Create();
    }
}

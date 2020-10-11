namespace Munchkin.Console
{
    public interface IFlowFactory<TContext>
    {
        IFlowContext<TContext> Create();
    }
}

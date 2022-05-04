namespace Munchkin.Core.Primitives
{
    public interface IDecisionGraphBuilder<TState>
    {
        IDecisionGraph<TState> Build();
    }
}

namespace Munchkin.Core.Primitives
{
    public interface IDecisionTree<TState>
    {
        Task<TState> ExecuteAsync(TState state);
    }
}

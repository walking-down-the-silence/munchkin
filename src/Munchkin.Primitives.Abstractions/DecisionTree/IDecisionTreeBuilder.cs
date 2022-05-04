namespace Munchkin.Core.Primitives
{
    public interface IDecisionTreeBuilder<TState>
    {
        IDecisionTree<TState> Build();
    }
}

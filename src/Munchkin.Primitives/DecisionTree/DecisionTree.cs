namespace Munchkin.Core.Primitives
{
    public class DecisionTree<TState> : IDecisionTree<TState>
    {
        private readonly Func<TState, Task<TState>> _func;

        public DecisionTree(Func<TState, Task<TState>> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public static IDecisionTreeContext<TState> Empty()
        {
            return new DecisionTreeBuilder<TState>();
        }

        public async Task<TState> ExecuteAsync(TState state)
        {
            if (state is null)
                throw new ArgumentNullException(nameof(state));

            return await _func.Invoke(state);
        }
    }
}

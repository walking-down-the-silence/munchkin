namespace Munchkin.Core.Primitives
{
    internal class DecisionTreeBuilder<TState> : IDecisionTreeContext<TState>
    {
        private readonly Func<TState, Task<TState>> _currentFunc;

        public DecisionTreeBuilder()
        {
            _currentFunc = new Func<TState, Task<TState>>(table => Task.FromResult(table));
        }

        public DecisionTreeBuilder(Func<TState, Task<TState>> func)
        {
            _currentFunc = func ?? throw new ArgumentNullException(nameof(func));
        }

        public IDecisionTree<TState> Build()
        {
            return new DecisionTree<TState>(_currentFunc);
        }

        public IDecisionTreeBuilder<TState> Condition(
            Func<TState, Task<bool>> condition,
            Func<IDecisionTreeContext<TState>, IDecisionTreeBuilder<TState>> branch1,
            Func<IDecisionTreeContext<TState>, IDecisionTreeBuilder<TState>> branch2)
        {
            if (condition is null)
                throw new ArgumentNullException(nameof(condition));

            if (branch1 is null)
                throw new ArgumentNullException(nameof(branch1));

            if (branch2 is null)
                throw new ArgumentNullException(nameof(branch2));

            var branch1Func = branch1.Invoke(this).Build();
            var branch2Func = branch2.Invoke(this).Build();

            var nextFunc = new Func<TState, Task<TState>>(async table =>
            {
                var result = await condition.Invoke(table);

                return result
                    ? await branch1Func.ExecuteAsync(table)
                    : await branch2Func.ExecuteAsync(table);
            });

            return new DecisionTreeBuilder<TState>(nextFunc);
        }

        public IDecisionTreeContext<TState> Then(IStep<TState> step)
        {
            if (step is null)
                throw new ArgumentNullException(nameof(step));

            var nextFunc = new Func<TState, Task<TState>>(async table =>
            {
                table = await _currentFunc.Invoke(table);
                return await step.Resolve(table);
            });

            return new DecisionTreeBuilder<TState>(nextFunc);
        }
    }
}

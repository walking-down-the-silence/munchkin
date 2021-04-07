using Munchkin.Core.Model;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Stages
{
    public class DecisionTree
    {
        private readonly Func<Table, Task<Table>> _func;

        private DecisionTree(Func<Table, Task<Table>> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public static IDecisionTreeContextBuilder Empty()
        {
            return new DecisionTreeBuilder();
        }

        public async Task<Table> ExecuteAsync(Table table)
        {
            if (table is null)
                throw new ArgumentNullException(nameof(table));

            return await _func.Invoke(table);
        }

        public async Task<Table> Resolve(IStep<Table> table)
        {
            throw new NotImplementedException();
        }

        private class DecisionTreeBuilder : IDecisionTreeContextBuilder
        {
            private Func<Table, Task<Table>> _currentFunc;

            public DecisionTreeBuilder()
            {
                _currentFunc = new Func<Table, Task<Table>>(table => Task.FromResult(table));
            }

            public DecisionTreeBuilder(Func<Table, Task<Table>> func)
            {
                _currentFunc = func ?? throw new ArgumentNullException(nameof(func));
            }

            public DecisionTree Build() => new(_currentFunc);

            public IDecisionTreeBuilder Condition(
                Func<Table, Task<bool>> condition,
                Func<IDecisionTreeContextBuilder, IDecisionTreeBuilder> branch1,
                Func<IDecisionTreeContextBuilder, IDecisionTreeBuilder> branch2)
            {
                if (condition is null)
                    throw new ArgumentNullException(nameof(condition));

                if (branch1 is null)
                    throw new ArgumentNullException(nameof(branch1));

                if (branch2 is null)
                    throw new ArgumentNullException(nameof(branch2));

                var branch1Func = branch1.Invoke(this).Build()._func;
                var branch2Func = branch2.Invoke(this).Build()._func;

                var nextFunc = new Func<Table, Task<Table>>(async table =>
                {
                    var result = await condition.Invoke(table);

                    return result
                        ? await branch1Func.Invoke(table)
                        : await branch2Func.Invoke(table);
                });

                return new DecisionTreeBuilder(nextFunc);
            }

            public IDecisionTreeContextBuilder Then(IStep<Table> step)
            {
                if (step is null)
                    throw new ArgumentNullException(nameof(step));

                var nextFunc = new Func<Table, Task<Table>>(async table =>
                {
                    table = await _currentFunc.Invoke(table);
                    return await step.Resolve(table);
                });

                return new DecisionTreeBuilder(nextFunc);
            }

            public ITransitionGraphBuilder Transition(Action<ITransitionBuilder> transiftionConfig)
            {
                if (transiftionConfig is null)
                {
                    throw new ArgumentNullException(nameof(transiftionConfig));
                }

                return this;
            }
        }
    }
}

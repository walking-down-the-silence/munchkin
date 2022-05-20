using Munchkin.Core.Model;
using System.Collections.Immutable;

namespace Munchkin.Core.Contracts
{
    public interface IMemento<TState>
    {
        TState Apply(TState state);

        TState Revert();
    }

    public class InMemoryTableMemento : IMemento<Table>
    {
        private ImmutableStack<Table> _states = ImmutableStack<Table>.Empty;

        public Table Apply(Table state)
        {
            _states = _states.Push(state);
            return state;
        }

        public Table Revert()
        {
            _states = _states.Pop(out var table);
            return table;
        }
    }
}

using Munchkin.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Actions
{
    public abstract class MultiShotAction : DynamicAction, IMultiShotAction<Table>
    {
        private int _executionsLeft;

        protected MultiShotAction(int executionCount, string title, string description) : base(title, description)
        {
            _executionsLeft = executionCount;
            ExecutionsCount = executionCount;
        }

        public int ExecutionsCount { get; }

        public int ExecutionsLeft => _executionsLeft;

        public override Task<Table> ExecuteAsync(Table table)
        {
            Interlocked.Decrement(ref _executionsLeft);
            return Task.FromResult(table);
        }
    }
}

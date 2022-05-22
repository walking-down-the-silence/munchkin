using Munchkin.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Actions
{
    public abstract record MultiShotAction(string Type, string Title, string Description, int ExecutionsCount) :
        DynamicAction(Type, Title, Description),
        IMultiShotAction<Table>
    {
        private int _executionsLeft;

        public int ExecutionsLeft => _executionsLeft;

        protected override Task<Table> OnBeforeExecuteAsync(Table table)
        {
            Interlocked.Decrement(ref _executionsLeft);
            return Task.FromResult(table);
        }
    }
}

using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Actions
{
    public abstract class DynamicAction : ActionBase, IAction<Table>
    {
        protected DynamicAction(string type, string title) :
            base(type, title)
        {
        }

        public bool CanExecute(Table table)
        {
            return OnCanExecute(table);
        }

        public async Task<Table> ExecuteAsync(Table table)
        {
            table = await OnBeforeExecuteAsync(table);
            table = await OnExecuteAsync(table);
            table = await OnAfterExecuteAsync(table);
            return table;
        }

        protected virtual bool OnCanExecute(Table table) => true;

        protected virtual Task<Table> OnBeforeExecuteAsync(Table table) => Task.FromResult(table);

        protected virtual Task<Table> OnExecuteAsync(Table table) => Task.FromResult(table);

        protected virtual Task<Table> OnAfterExecuteAsync(Table table) => Task.FromResult(table);
    }
}

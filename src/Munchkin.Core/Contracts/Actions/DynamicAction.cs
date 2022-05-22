using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Actions
{
    public abstract record DynamicAction(string Type, string Title, string Description) :
        ActionBase(Type, Title, Description),
        IAction<Table>
    {
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


        protected abstract bool OnCanExecute(Table table);

        protected virtual Task<Table> OnBeforeExecuteAsync(Table table) => Task.FromResult(table);

        protected virtual Task<Table> OnExecuteAsync(Table table) => Task.FromResult(table);

        protected virtual Task<Table> OnAfterExecuteAsync(Table table) => Task.FromResult(table);
    }
}

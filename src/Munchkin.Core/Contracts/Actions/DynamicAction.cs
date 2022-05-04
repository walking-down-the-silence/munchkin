using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Actions
{
    public abstract record DynamicAction(string Type, string Title, string Description) :
        ActionBase(Type, Title, Description),
        IAction<Table>
    {
        public abstract bool CanExecute(Table state);

        public abstract Task<Table> ExecuteAsync(Table state);
    }
}

using Munchkin.Core.Contracts;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class ResetActionsStep : ITerminalStep<Table>
    {
        public Task<Table> Resolve(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}

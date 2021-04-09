using Munchkin.Core.Contracts.Stages;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class ResetActionsStep : TerminalStep<Table>
    {
        public override Task<Table> Resolve(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}

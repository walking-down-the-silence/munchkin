using Munchkin.Core.Contracts.Stages;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class ResetActionsStep : StepBase<Table>
    {
        public ResetActionsStep() : base(StepNames.ResetActions)
        {

        }

        protected override async Task<Table> OnResolve(Table table)
        {
            throw new System.NotImplementedException();
        }
    }
}

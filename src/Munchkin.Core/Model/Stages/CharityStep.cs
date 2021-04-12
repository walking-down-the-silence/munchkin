using Munchkin.Core.Contracts.Stages;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class CharityStep : StepBase<Table>
    {
        public CharityStep() : base(StepNames.Charity)
        {
        }

        protected override async Task<Table> OnResolve(Table table)
        {
            // TODO: implement the charity loop
            var stage = new EndStep();
            return await stage.Resolve(table);
        }
    }
}

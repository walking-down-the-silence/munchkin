using Munchkin.Core.Contracts.Stages;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class DeathStep : StepBase<Table>
    {
        public DeathStep() : base(StepNames.Death)
        {
        }

        protected override async Task<Table> OnResolve(Table table)
        {
            // TODO: allow other players to take a card from dead players avatar
            var end = new EndStep();
            return await end.Resolve(table);
        }
    }
}

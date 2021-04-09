using Munchkin.Core.Contracts.Stages;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class CharityStep : HierarchialStep<Table>
    {
        public override async Task<Table> Resolve(Table table)
        {
            // TODO: implement the charity loop
            var stage = new EndStep();
            return await stage.Resolve(table);
        }
    }
}

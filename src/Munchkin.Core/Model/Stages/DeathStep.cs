using Munchkin.Core.Contracts;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class DeathStep : HierarchialStep<Table>
    {
        public override async Task<Table> Resolve(Table table)
        {
            // TODO: allow other players to take a card from dead players avatar
            var end = new EndStep();
            return await end.Resolve(table);
        }
    }
}

using Munchkin.Core.Contracts;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class DeathStep : IHierarchialStep<Table>
    {
        public async Task<Table> Resolve(Table table)
        {
            // TODO: allow other players to take a card
            var end = new EndStep(null);
            return await end.Resolve(table);
        }
    }
}

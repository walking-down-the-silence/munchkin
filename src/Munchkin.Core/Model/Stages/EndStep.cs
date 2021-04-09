using Munchkin.Core.Contracts.Stages;
using Munchkin.Core.Extensions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class EndStep : TerminalStep<Table>
    {
        public override Task<Table> Resolve(Table table)
        {
            table.Dungeon.PlayedCards.ForEach(card => card.Discard(table));
            table.Dungeon.Reset();
            return Task.FromResult(table);
        }
    }
}

using Munchkin.Core.Contracts.Stages;
using Munchkin.Core.Extensions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class EndStep : StepBase<Table>
    {
        public EndStep() : base(StepNames.End)
        {
        }

        protected override async Task<Table> OnResolve(Table table)
        {
            table.Dungeon.PlayedCards.ForEach(card => card.Discard(table));
            table.Dungeon.Reset();
            return await Task.FromResult(table);
        }
    }
}

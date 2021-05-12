using Munchkin.Core.Contracts.Stages;
using Munchkin.Core.Extensions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class PlayerTurnEndStep : StepBase<Table>
    {
        public PlayerTurnEndStep() : base(StepNames.PlayerTurnEnd)
        {
        }

        protected override Task<Table> OnResolve(Table table)
        {
            table.Dungeon.PlayedCards.ForEach(card => card.Discard(table));
            table.Dungeon.Reset();
            return Task.FromResult(table);
        }
    }
}

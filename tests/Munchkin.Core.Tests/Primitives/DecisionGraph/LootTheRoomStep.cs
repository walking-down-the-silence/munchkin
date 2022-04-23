using Munchkin.Core.Model;
using Munchkin.Core.Model.Phases;
using Munchkin.Core.Primitives;
using System.Threading.Tasks;

namespace Munchkin.Core.Tests.Primitives
{
    public class LootTheRoomStep : StepBase<Table>
    {
        public LootTheRoomStep() : base(StepNames.LootTheRoom)
        {
        }

        protected override async Task<Table> OnResolve(Table table)
        {
            // TODO: check if deck is empty and reshuffle discard if it is
            var doorsCard = table.DoorsCardDeck.Take();
            table.Players.Current.TakeInHand(doorsCard);

            var stage = new CharityStep();
            return await stage.Resolve(table);
        }
    }
}
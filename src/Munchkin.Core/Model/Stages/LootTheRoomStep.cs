using Munchkin.Core.Contracts.Stages;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    /// <summary>
    /// Takes a card from the doors deck and puts in hand.
    /// </summary>
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

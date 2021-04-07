using Munchkin.Core.Contracts;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    /// <summary>
    /// Takes a card from the doors deck and puts in hand.
    /// </summary>
    public class LootTheRoomStep : HierarchialStep<Table>
    {
        public override async Task<Table> Resolve(Table table)
        {
            // TODO: check if deck is empty and reshuffle discard if it is
            var doorsCard = table.DoorsCardDeck.Take();
            table.Players.Current.TakeInHand(doorsCard);

            var stage = new CharityStep();
            return await stage.Resolve(table);
        }
    }
}

using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class SnailsOnSpeed : MonsterCard
    {
        public SnailsOnSpeed() :
            base(MunchkinDeluxeCards.Doors.SnailsOnSpeed, "Snails On Speed", 4, 1, 2, -2, false)
        {
        }

        public async override Task BadStuff(Table state)
        {
            // TODO: double check this cards logic
            //var diceRollResult = Dice.Roll();

            //var action = await new LoseItemsOrCardsInHandRequest(state.Players.Current, state)
            //    .SendAsync(state);

            //if (action == LoseItemsOrCardsInHandActions.LoseItems)
            //{
            //    var itemCards = state.Players.Current.Equipped.OfType<ItemCard>().ToList();

            //    if (itemCards.Any())
            //    {
            //        var cardsToDiscard = await new PlayerSelectMultipleCardsRequest(state.Players.Current, state, itemCards, diceRollResult)
            //            .SendAsync(state);

            //        cardsToDiscard.ForEach(card => card.Discard(state));
            //    }
            //}
            //else
            //{
            //    var handCards = state.Players.Current.YourHand.ToList();

            //    if (handCards.Any())
            //    {

            //        var cardsToDiscard = await new PlayerSelectMultipleCardsRequest(state.Players.Current, state, handCards, diceRollResult)
            //           .SendAsync(state);

            //        cardsToDiscard.ForEach(card => card.Discard(state));
            //    }
            //}
        }
    }
}
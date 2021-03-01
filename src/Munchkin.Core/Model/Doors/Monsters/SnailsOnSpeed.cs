using System;
using System.Linq;
using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Requests;
using Munchkin.Core.Model.Requests.Enums;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class SnailsOnSpeed : MonsterCard
    {
        public SnailsOnSpeed() : base("Snails On Speed", 4, 1, 2, -2, false)
        {
        }

        public async override Task BadStuff(Table state)
        {
            // TODO: add logic to chose between lose items or cards in hand
            var diceRollResult = Dice.Roll();

            var request = new LoseItemsOrCardsInHandRequest(state.Players.Current, state);
            var response = await state.RequestSink.Send(request);
            var action = await response.Task;

            if (action == LoseItemsOrCardsInHandActions.LoseItems)
            {
                for(var i = 1; i <= diceRollResult; i++)
                {
                    var itemCards = state.Players.Current.Equipped.OfType<ItemCard>().ToList();
                    var selectCardsRequest = new SelectCardsRequest(state.Players.Current, state, itemCards);
                    var selectCardsResponse = await state.RequestSink.Send(selectCardsRequest);
                    var card = await selectCardsResponse.Task;
                    state.Players.Current.Discard(state, card);
                }
            }
            else
            {
                for (var i = 1; i <= diceRollResult; i++)
                {
                    var handCards = state.Players.Current.YourHand.ToList();
                    var selectCardsRequest = new SelectCardsRequest(state.Players.Current, state, handCards);
                    var selectCardsResponse = await state.RequestSink.Send(selectCardsRequest);
                    var card = await selectCardsResponse.Task;
                    state.Players.Current.Discard(state, card);
                }
            }
        }
    }
}
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using System.Linq;

namespace Munchkin.Core.Model.Stages
{
    public static class Salvage
    {
        public static SalvageState From(Player player)
        {
            // TODO: Ensure that player avatar is dead when reaching this point
            return new SalvageState(player, player.AllCards());
        }

        public static SalvageState TakeCard(SalvageState death, Player taker, Card card)
        {
            //// TODO: send a request to each player to select the cards from dead player
            //foreach (var targetPlayer in table.Players.Where(player => !player.IsDead))
            //{
            //    var selectCardRequest = new PlayerSelectSingleCardRequest(targetPlayer, table, player.YourHand);
            //    var selectCardResponse = await table.RequestSink.Send(selectCardRequest);
            //    var selectedCard = await selectCardResponse.Task;

            death.Player.Discard(card);
            taker.PutInBackpack(card);

            //}

            return death with
            {
                Cards = death.Cards.Where(x => !x.Equals(card)).ToArray()
            };
        }
    }
}

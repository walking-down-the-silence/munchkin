using Munchkin.Core.Contracts.Actions;
using System;
using System.Threading.Tasks;
using static Munchkin.Core.Model.Cards.MunchkinDeluxeCards;

namespace Munchkin.Core.Model.Actions
{
    internal record ThiefTheftAction() :
        DynamicAction(ThiefClass.Theft, "Theft", "Try To Steal A Card")
    {
        protected override bool OnCanExecute(Table table)
        {
            throw new NotImplementedException();
        }

        protected override Task<Table> OnExecuteAsync(Table table)
        {
            throw new NotImplementedException();
            // TODO: if stabbing failed, can player try and stab once more?
            //Shots--;

            //var playerSelectionRequest = state.RequestSink.Request<Player>(Player, Player, state.Players);
            //var selectedPlayer = await playerSelectionRequest.GetResult();
            //var stealableCards = selectedPlayer.Equipped.Concat(Player.Backpack);

            //var diceRoll = Dice.Roll;
            //// TODO: include dice roll subtraction from curses

            //if (diceRoll > 3)
            //{
            //    var response = state.RequestSink.Request<Card>(Player, Player, stealableCards);
            //    var card = await response.GetResult();
            //    selectedPlayer.Discard(card);
            //    Player.TakeInHand(card);
            //}
        }
    }
}
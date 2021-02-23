using Munchkin.Core.Contracts.Actions;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Actions
{
    internal class ThiefStealCardAction : DynamicAction
    {
        public ThiefStealCardAction() : base("Steal Card", "")
        {
        }

        public override bool CanExecute(Table state)
        {
            throw new NotImplementedException();
        }

        public override Task<Table> ExecuteAsync(Table state)
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
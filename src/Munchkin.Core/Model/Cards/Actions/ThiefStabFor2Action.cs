using Munchkin.Core.Contracts.Actions;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Actions
{
    internal record ThiefStabFor2Action() :
        DynamicAction(string.Empty, "Stab (-2)", string.Empty)
    {
        public override bool CanExecute(Table state)
        {
            throw new NotImplementedException();
        }

        public override Task<Table> ExecuteAsync(Table state)
        {
            throw new NotImplementedException();
            //var playerCards = Player.Equipped
            //    .Concat(Player.YourHand)
            //    .Concat(Player.Backpack);

            //var response = gameContext.RequestSink.Request<Card>(Player, Player, playerCards);
            //var card = await response.GetResult();
            //Player.Discard(card);

            //var diceRoll = Dice.Roll;
            //// TODO: include dice roll subtraction from curses

            //if (diceRoll > 3)
            //{
            //    // TODO: discard as -2 strength
            //    await gameContext.Dungeon.PlayACard(card);
            //}
            //else
            //{
            //    // TODO: simply discard
            //    await gameContext.Dungeon.PlayACard(card);
            //}
        }
    }
}
using Munchkin.Core.Contracts.Actions;
using System;
using System.Threading.Tasks;
using static Munchkin.Core.Model.Cards.MunchkinDeluxeCards;

namespace Munchkin.Core.Model.Actions
{
    internal record ThiefBackstabbingAction() :
        DynamicAction(ThiefClass.Backstabbing, "Backstabbing", "Stab (-2)")
    {
        protected override bool OnCanExecute(Table table)
        {
            throw new NotImplementedException();
        }

        protected override Task<Table> OnExecuteAsync(Table table)
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
using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Model;
using System;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Actions
{
    internal class ThiefStabFor2Action : DynamicAction
    {
        public ThiefStabFor2Action() : base("Stab (-2)", "")
        {
        }

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
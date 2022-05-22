using Munchkin.Core.Contracts.Actions;
using System;
using System.Threading.Tasks;
using static Munchkin.Core.Model.Cards.MunchkinDeluxeCards;

namespace Munchkin.Core.Model.Actions
{
    internal record WarriorBerserkingAction() :
        MultiShotAction(WarriorClass.Berserking, "Berserking", "Bonus (+1)", 3)
    {
        protected override bool OnCanExecute(Table table) => ExecutionsLeft > 0;

        protected override Task<Table> OnExecuteAsync(Table table)
        {
            throw new NotImplementedException();
            //Shots--;
            //var playerCards = Player.Equipped
            //    .Concat(Player.YourHand)
            //    .Concat(Player.Backpack);

            //var response = state.RequestSink.Request<Card>(Player, Player, playerCards);
            //var card = await response.GetResult();
            //Player.Discard(card);
            //await state.Dungeon.PlayACard(card);
        }
    }
}
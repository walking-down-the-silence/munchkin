using Munchkin.Core.Contracts.Actions;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Actions
{
    internal class WarriorStrengthBonus1Action : MultiShotAction
    {
        public WarriorStrengthBonus1Action() : base(3, "Rage (+1)", "")
        {
        }

        public override bool CanExecute(Table state) => ExecutionsLeft > 0;

        public override async Task<Table> ExecuteAsync(Table table)
        {
            table = await base.ExecuteAsync(table);

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
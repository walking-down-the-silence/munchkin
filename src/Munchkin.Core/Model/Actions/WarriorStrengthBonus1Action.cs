using Munchkin.Core.Contracts.Actions;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Actions
{
    internal class WarriorStrengthBonus1Action : DynamicAction
    {
        public WarriorStrengthBonus1Action() : base("Rage (+1)", "")
        {
            Shots = 3;
        }

        /// <summary>
        /// Gets how many timer per turn and action can be executed
        /// </summary>
        public int Shots { get; }

        public override bool CanExecute(Table state)
        {
            throw new NotImplementedException();
        }

        public override Task<Table> ExecuteAsync(Table state)
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
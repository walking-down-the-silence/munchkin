using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Actions
{
    /// <summary>
    /// Loots the room before exiting
    /// </summary>
    public class LootTheRoomAction : DynamicAction
    {
        public LootTheRoomAction() : base("Loot The Room", "")
        {
        }

        public override bool CanExecute(Table state)
        {
            throw new NotImplementedException();
        }

        public override async Task<Table> ExecuteAsync(Table state)
        {
            // loot the room by drawing a door card
            DoorsCard card = state.DoorsCardDeck.Take();
            state.Players.Current.TakeInHand(card);
            //state.Dungeon.ExitTheDungeon();
            return state;
        }
    }
}
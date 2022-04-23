using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Phases
{
    public static class DungeonExtensions
    {
        public static IState KickOpenTheDoor(this Dungeon state, Table table)
        {
            // NOTE: 'Kick Open the Door' by drawing a card from the Doors Deck
            var doors = table.DoorsCardDeck.Take();

            var nextState = doors switch
            {
                CurseCard curse => CursedRoom.From(table, table.Players.Current, curse),
                MonsterCard monster => CombatRoom.From(table, table.Players.Current, monster),
                _ => DungeonExtensions.TakeInHand(table, doors, table.Players.Current)
            };

            return nextState;
        }

        /// <summary>
        /// If acquired some other way, such as by Looting The Room, Curse cards
        /// go into your hand and may be played on any player at any time.
        /// </summary>
        private static IState TakeInHand(Table table, DoorsCard doors, Player currentPlayer)
        {
            // NOTE: if card is taking in hand then remove from played, so it is not discarded later
            table.Players.Current.TakeInHand(doors);
            return EmptyRoom.From(table, currentPlayer);
        }
    }
}
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Phases.Events;

namespace Munchkin.Core.Model.Phases
{
    /// <summary>
    /// Defines the state of the dungeon that the player has entered.
    /// </summary>
    public static class Dungeon
    {
        /// <summary>
        /// Kick open the door into the dungeon.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static Table KickOpenTheDoor(Table table)
        {
            // NOTE: 'Kick Open the Door' by drawing a card from the Doors Deck
            table = table.TakeDoor(out var doors);

            var kickOpenedTheDoorEvent = new KickOpenedTheDoorEvent(table.Players.Current.Nickname, doors.GetHashCode().ToString());
            table.ActionLog.Add(kickOpenedTheDoorEvent);

            table = doors switch
            {
                CurseCard curse => CreateCursedRoom(table, table.Players.Current, curse),
                MonsterCard monster => CreateCombatRoom(table, monster),
                _ => CreateEmptyRoom(table, doors)
            };

            return table;
        }

        /// <summary>
        /// Get another door from the deck.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static Table LootTheRoom(Table table)
        {
            // NOTE: 'Loot the Room' by drawing another cards from the Doors Deck
            table = table.TakeDoor(out var doors);
            table.Players.Current.TakeInHand(doors);

            var playerTookInHandEvent = new PlayerTookInHandEvent(table.Players.Current.Nickname, doors.GetHashCode().ToString());
            table.ActionLog.Add(playerTookInHandEvent);

            return table;
        }

        /// <summary>
        /// Play a monster from hand.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="table"></param>
        /// <param name="monster"></param>
        /// <returns></returns>
        public static Table LookForTrouble(Table table, MonsterCard monster)
        {
            return CreateCombatRoom(table, monster);
        }

        /// <summary>
        /// TODO: maybe return the curse state so that the caller can save it?
        /// </summary>
        /// <param name="state"></param>
        /// <param name="table"></param>
        /// <param name="curse"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public static Table Curse(Table table, CurseCard curse, Player player)
        {
            return CreateCursedRoom(table, player, curse);
        }

        private static Table CreateCursedRoom(Table table, Player player, CurseCard curse)
        {
            table = table.Play(curse);

            // TODO: use the real card id
            var playerCursedEvent = new PlayerCursedEvent(player.Nickname, curse.GetHashCode().ToString());
            table.ActionLog.Add(playerCursedEvent);

            return table;
        }

        private static Table CreateCombatRoom(Table table, MonsterCard monster)
        {
            table = table.Play(monster);

            // TODO: use the real card id
            var combatEvent = new CombatStartedEvent(table.Players.Current.Nickname, monster.GetHashCode().ToString());
            table.ActionLog.Add(combatEvent);

            return table;
        }

        private static Table CreateEmptyRoom(Table table, DoorsCard card)
        {
            // NOTE: If acquired some other way, such as by Looting The Room, Curse cards
            // go into your hand and may be played on any player at any time.
            table.Players.Current.TakeInHand(card);

            var playerHandEvent = new PlayerTookInHandEvent(table.Players.Current.Nickname, card.GetHashCode().ToString());
            table.ActionLog.Add(playerHandEvent);

            return table;
        }
    }
}
﻿using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Phases.Events;
using System;

namespace Munchkin.Core.Model.Phases
{
    /// <summary>
    /// Defines a set of action available in the dungeon that the player has entered.
    /// </summary>
    public static class Dungeon
    {
        /// <summary>
        /// Kick open the door into the dungeon.
        /// </summary>
        /// <param name="table">The table where the game takes place.</param>
        /// <returns>Returns an updated instance of the table.</returns>
        public static Table KickOpenTheDoor(Table table)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));

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
        /// Draw another door card from the deck into the hand.
        /// </summary>
        /// <param name="table">The table where the game takes place.</param>
        /// <returns>Returns an updated instance of the table.</returns>
        public static Table LootTheRoom(Table table)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));

            // NOTE: 'Loot the Room' by drawing another cards from the Doors Deck
            table = table.TakeDoor(out var doors);
            table.Players.Current.TakeInHand(doors);

            var playerTookInHandEvent = new PlayerTookInHandEvent(table.Players.Current.Nickname, doors.GetHashCode().ToString());
            table.ActionLog.Add(playerTookInHandEvent);

            return table;
        }

        /// <summary>
        /// Play a monster from the hand.
        /// </summary>
        /// <param name="table">The table where the game takes place.</param>
        /// <param name="monster">The monster card played.</param>
        /// <returns>Returns an updated instance of the table.</returns>
        public static Table LookForTrouble(Table table, MonsterCard monster)
        {
            return CreateCombatRoom(table, monster);
        }

        /// <summary>
        /// Play a curse on the player.
        /// </summary>
        /// <param name="table">The table where the game takes place.</param>
        /// <param name="curse">The curse card played.</param>
        /// <param name="player">The player to curse.</param>
        /// <returns>Returns an updated instance of the table.</returns>
        public static Table Curse(Table table, CurseCard curse, Player player)
        {
            return CreateCursedRoom(table, player, curse);
        }

        private static Table CreateCursedRoom(Table table, Player player, CurseCard curse)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));
            ArgumentNullException.ThrowIfNull(curse, nameof(curse));

            table = table.Play(curse);

            // TODO: use the real card id
            var playerCursedEvent = new PlayerCursedEvent(player.Nickname, curse.GetHashCode().ToString());
            table.ActionLog.Add(playerCursedEvent);

            return table;
        }

        private static Table CreateCombatRoom(Table table, MonsterCard monster)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(monster, nameof(monster));

            table = table.Play(monster);

            // TODO: use the real card id
            var combatEvent = new CombatStartedEvent(table.Players.Current.Nickname, monster.GetHashCode().ToString());
            table.ActionLog.Add(combatEvent);

            return table;
        }

        private static Table CreateEmptyRoom(Table table, DoorsCard card)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(card, nameof(card));

            // NOTE: If acquired some other way, such as by Looting The Room, Curse cards
            // go into your hand and may be played on any player at any time.
            table.Players.Current.TakeInHand(card);

            var playerHandEvent = new PlayerTookInHandEvent(table.Players.Current.Nickname, card.GetHashCode().ToString());
            table.ActionLog.Add(playerHandEvent);

            return table;
        }
    }
}
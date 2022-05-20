using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Phases.Events;
using System;

namespace Munchkin.Core.Model.Phases
{
    /// <summary>
    /// Defines the actions when running away from the monster in combat.
    /// </summary>
    public static class RunningAway
    {
        /// <summary>
        /// Roll the dice to get the number that indicates if the run away was successful.
        /// </summary>
        /// <param name="table">The table where the game takes place.</param>
        /// <param name="monster">The monster that the player is running away from.</param>
        /// <param name="player">The player who is running away.</param>
        /// <returns>Returns an updated instance of the table.</returns>
        public static Table RollTheDice(Table table, MonsterCard monster, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(monster, nameof(monster));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            var diceRollEvent = new RunningAwayFromMonsterDiceRollEvent(player.Nickname, monster.Code, Dice.Roll());
            table.ActionLog.Add(diceRollEvent);

            return table;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table">The table where the game takes place.</param>
        /// <param name="monster">The monster to take bad stuff from.</param>
        /// <param name="taker">The player who is taking the bad stuff.</param>
        /// <returns>Returns an updated instance of the table.</returns>
        public static Table TakeBadStuff(Table table, MonsterCard monster, Player taker)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(monster, nameof(monster));
            ArgumentNullException.ThrowIfNull(taker, nameof(taker));

            if (!taker.IsDead())
                monster.BadStuff(table);

            return table;
        }
    }
}
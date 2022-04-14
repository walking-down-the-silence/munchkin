using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Attributes;
using System;
using System.Linq;

namespace Munchkin.Core.Model
{
    public static class DungeonExtensions
    {
        #region Dungeon Modifiers

        /// <summary>
        /// Gets an attribute of a specific type.
        /// </summary>
        /// <typeparam name="TAttribute">Type of the attribute.</typeparam>
        /// <returns>The attribute instance.</returns>
        public static TAttribute GetAttribute<TAttribute>(this Dungeon dungeon)
            where TAttribute : Contracts.Attributes.Attribute =>
            dungeon.Attributes.OfType<TAttribute>().FirstOrDefault();

        /// <summary>
        /// Add an attribute of a specific type.
        /// </summary>
        /// <typeparam name="TAttribute">Type of the attribute.</typeparam>
        /// <param name="attribute">The attribute instance.</param>
        public static Dungeon AddAtribute<TAttribute>(this Dungeon dungeon, TAttribute attribute)
            where TAttribute : Contracts.Attributes.Attribute =>
            dungeon with { Attributes = dungeon.Attributes.Add(attribute) };

        /// <summary>
        /// Adds a card that has been played to the collection.
        /// </summary>
        /// <param name="dungeon">The dungeon in which the action takes place.</param>
        /// <param name="card">The card that has been played.</param>
        /// <returns></returns>
        public static Dungeon AddPlayedCard(this Dungeon dungeon, Card card) =>
            dungeon with { PlayedCards = dungeon.PlayedCards.Add(card) };

        /// <summary>
        /// Removes a card that has been played from the collection.
        /// </summary>
        /// <param name="dungeon">The dungeon in which the action takes place.</param>
        /// <param name="card">The card that has been played.</param>
        /// <returns></returns>
        public static Dungeon RemovePlayedCard(this Dungeon dungeon, Card card) =>
            dungeon with { PlayedCards = dungeon.PlayedCards.Remove(card) };

        #endregion

        #region Utilities

        /// <summary>
        /// Aggregates the values of all attributes of a specific type using selector.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to filter by.</typeparam>
        /// <param name="dungeon">The dungeon in which the action takes place.</param>
        /// <param name="selector">The selector function to get the value from the attribute.</param>
        /// <returns></returns>
        public static int AggregateAttributes<TAttribute>(this Dungeon dungeon, Func<TAttribute, int> selector) =>
            dungeon.Attributes
                .OfType<TAttribute>()
                .Aggregate(0, (total, next) => total + selector(next));

        /// <summary>
        /// Checks if player is winning the game by comparing strength of a player and monster.
        /// </summary>
        /// <param name="dungeon">The dungeon in which the action takes place.</param>
        /// <returns></returns>
        public static bool PlayersAreWinningCombat(this Dungeon dungeon)
        {
            int playerStrength = dungeon.AggregateAttributes<PlayerStrengthBonusAttribute>(x => x.Bonus);
            int mosterStrength = dungeon.AggregateAttributes<MonsterStrengthBonusAttribute>(x => x.Bonus);
            return playerStrength > mosterStrength;
        }

        #endregion

        #region Attribute Bonuses

        /// <summary>
        /// Monster strength if any present.
        /// </summary>
        public static int MonsterStrength(this Dungeon dungeon) =>
            dungeon.AggregateAttributes<MonsterStrengthBonusAttribute>(x => x.Bonus);

        /// <summary>
        /// Heroes strength that entered the dungeon.
        /// </summary>
        public static int PlayersStrength(this Dungeon dungeon) =>
            dungeon.AggregateAttributes<PlayerStrengthBonusAttribute>(x => x.Bonus);

        /// <summary>
        /// Bonus to run away from dungeon.
        /// </summary>
        public static int RunAwayBonus(this Dungeon dungeon) =>
            dungeon.AggregateAttributes<RunAwayBonusAttribute>(x => x.Bonus);

        /// <summary>
        /// Levels gained if dungeon is cleared.
        /// </summary>
        public static int RewardLevels(this Dungeon dungeon) =>
            dungeon.AggregateAttributes<RewardLevelsAttribute>(x => x.Bonus);

        /// <summary>
        /// Treasures gained if dungeon is cleared.
        /// </summary>
        public static int RewardTreasures(this Dungeon dungeon) =>
            dungeon.AggregateAttributes<RewardTreasuresAttribute>(x => x.Bonus);

        #endregion

        #region Available Actions

        public static Dungeon TakeInHand(this Dungeon dungeon, Table table)
        {
            // NOTE: if card is taking in hand then remove from played, so it is not discarded later
            table.Dungeon.RemovePlayedCard(dungeon.Door);
            table.Players.Current.TakeInHand(dungeon.Door);
            return dungeon with
            {
                Door = null,
                PlayedCards = dungeon.PlayedCards.Add(dungeon.Door)
            };
        }

        public static Dungeon PutInPlay(this Dungeon dungeon, Table table)
        {
            // NOTE: if card not taken in hand, then it should be put in play
            table.Dungeon.AddPlayedCard(dungeon.Door);
            return dungeon;
        }

        #endregion
    }
}
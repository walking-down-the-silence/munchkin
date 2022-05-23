using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Attributes;
using Munchkin.Core.Model.Attributes;
using System;
using System.Linq;

namespace Munchkin.Core.Extensions
{
    public static class SupportAttributes
    {
        #region Attribute Operators

        /// <summary>
        /// Gets if the attribute of the specified type is present within the object.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute.</typeparam>
        /// <param name="attributes">The object that implement the <see cref="ISupportAttributes"/> interface.</param>
        /// <returns></returns>
        public static bool HasAttribute<TAttribute>(this ISupportAttributes attributes)
            where TAttribute : IAttribute
        {
            return attributes is not null && attributes.Attributes.OfType<TAttribute>().Any();
        }

        /// <summary>
        /// Gets an attribute of a specific type.
        /// </summary>
        /// <typeparam name="TAttribute">Type of the attribute.</typeparam>
        /// <param name="attributes">The object that implement the <see cref="ISupportAttributes"/> interface.</param>
        /// <returns>The attribute instance.</returns>
        public static TAttribute GetAttribute<TAttribute>(this ISupportAttributes attributes)
            where TAttribute : class, IAttribute
        {
            return attributes?.Attributes?.OfType<TAttribute>()?.FirstOrDefault();
        }

        /// <summary>
        /// Aggregates the values of all attributes of a specific type using selector.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to filter by.</typeparam>
        /// <param name="attributes">The object that implement the <see cref="ISupportAttributes"/> interface.</param>
        /// <param name="selector">The selector function to get the value from the attribute.</param>
        /// <returns></returns>
        public static int AggregateAttributes<TAttribute>(this ISupportAttributes attributes, Func<TAttribute, int> selector)
        {
            return attributes.Attributes
                .OfType<TAttribute>()
                .Aggregate(0, (total, next) => total + selector(next));
        }

        #endregion

        #region Attribute Bonuses

        /// <summary>
        /// Monster strength if any present.
        /// </summary>
        public static int MonsterStrength(this ISupportAttributes attributes) =>
            attributes.AggregateAttributes<MonsterStrengthBonusAttribute>(x => x.Bonus);

        /// <summary>
        /// Players strength if any present.
        /// </summary>
        public static int PlayersStrength(this ISupportAttributes attributes) =>
            attributes.AggregateAttributes<PlayerStrengthBonusAttribute>(x => x.Bonus);

        /// <summary>
        /// RewardTreasures to run away from dungeon.
        /// </summary>
        public static int RunAwayBonus(this ISupportAttributes attributes) =>
            attributes.AggregateAttributes<RunAwayBonusAttribute>(x => x.Bonus);

        /// <summary>
        /// Levels gained if dungeon is cleared.
        /// </summary>
        public static int RewardLevels(this ISupportAttributes attributes) =>
            attributes.AggregateAttributes<RewardLevelsAttribute>(x => x.Bonus);

        /// <summary>
        /// Treasures gained if dungeon is cleared.
        /// </summary>
        public static int RewardTreasures(this ISupportAttributes attributes) =>
            attributes.AggregateAttributes<RewardTreasuresAttribute>(x => x.Bonus);

        #endregion
    }
}
using Munchkin.Core.Contracts.Attributes;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core.Extensions
{
    public static class CardExtensions
    {
        #region Utilities

        public static IEnumerable<Card> NotOfType<TExcept>(this IEnumerable<Card> list) =>
            list.Where(card => card is not TExcept);

        public static bool HasAttribute<TAttribute>(this Card card)
            where TAttribute : IAttribute =>
            card is not null && card.Attributes.OfType<TAttribute>().Any();

        /// <summary>
        /// Gets an attribute of a specific type.
        /// </summary>
        /// <typeparam name="TAttribute">Type of the attribute.</typeparam>
        /// <returns>The attribute instance.</returns>
        public static TAttribute GetAttribute<TAttribute>(this Card card)
            where TAttribute : class, IAttribute =>
            card?.Attributes?.OfType<TAttribute>()?.FirstOrDefault();

        /// <summary>
        /// Aggregates the values of all attributes of a specific type using selector.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to filter by.</typeparam>
        /// <param name="state">The dungeon in which the action takes place.</param>
        /// <param name="selector">The selector function to get the value from the attribute.</param>
        /// <returns></returns>
        public static int AggregateAttributes<TAttribute>(this Card card, System.Func<TAttribute, int> selector) =>
            card.Attributes
                .OfType<TAttribute>()
                .Aggregate(0, (total, next) => total + selector(next));

        #endregion

        #region Attribute Bonuses

        /// <summary>
        /// Monster strength if any present.
        /// </summary>
        public static int MonsterStrength(this Card card) =>
            card.AggregateAttributes<MonsterStrengthBonusAttribute>(x => x.Bonus);

        /// <summary>
        /// Bonus to run away from dungeon.
        /// </summary>
        public static int RunAwayBonus(this Card card) =>
            card.AggregateAttributes<RunAwayBonusAttribute>(x => x.Bonus);

        /// <summary>
        /// Levels gained if dungeon is cleared.
        /// </summary>
        public static int RewardLevels(this Card card) =>
            card.AggregateAttributes<RewardLevelsAttribute>(x => x.Bonus);

        /// <summary>
        /// Treasures gained if dungeon is cleared.
        /// </summary>
        public static int RewardTreasures(this Card card) =>
            card.AggregateAttributes<RewardTreasuresAttribute>(x => x.Bonus);

        #endregion

        public static void DiscardAll(this IEnumerable<Card> cards, Table table)
        {
            foreach (var card in cards)
            {
                card.Discard(table);
            }
        }
    }
}
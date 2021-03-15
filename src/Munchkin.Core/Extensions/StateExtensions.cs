using Munchkin.Core.Contracts.States;
using Munchkin.Core.Model.Attributes;
using System;
using System.Linq;

namespace Munchkin.Core.Extensions
{
    public static class StateExtensions
    {
        public static int AggregateProperties<TAttribute>(this IState state, Func<TAttribute, int> selector)
        {
            return state.Attributes.OfType<TAttribute>().Aggregate(0, (total, next) => total + selector(next));
        }

        public static bool PlayersAreWinningCombat(this IState state)
        {
            int playerStrength = state.AggregateProperties<PlayerStrengthBonusAttribute>(x => x.Bonus);
            int mosterStrength = state.AggregateProperties<MonsterStrengthBonusAttribute>(x => x.Bonus);
            return playerStrength > mosterStrength;
        }

        #region Attribute Bonuses

        /// <summary>
        /// Monster strength if any present.
        /// </summary>
        public static int MonsterStrength(this IState state) => state.AggregateProperties<MonsterStrengthBonusAttribute>(x => x.Bonus);

        /// <summary>
        /// Heroes strength that entered the dungeon.
        /// </summary>
        public static int PlayersStrength(this IState state) => state.AggregateProperties<PlayerStrengthBonusAttribute>(x => x.Bonus);

        /// <summary>
        /// Bonus to run away from dungeon.
        /// </summary>
        public static int RunAwayBonus(this IState state) => state.AggregateProperties<RunAwayBonusAttribute>(x => x.Bonus);

        /// <summary>
        /// Levels gained if dungeon is cleared.
        /// </summary>
        public static int RewardLevels(this IState state) => state.AggregateProperties<RewardLevelsAttribute>(x => x.Bonus);

        /// <summary>
        /// Treasures gained if dungeon is cleared.
        /// </summary>
        public static int RewardTreasures(this IState state) => state.AggregateProperties<RewardTreasuresAttribute>(x => x.Bonus);

        #endregion
    }
}

using Munchkin.Core.Contracts.Attributes;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using System.Collections.Immutable;
using System.Linq;

namespace Munchkin.Core.Contracts
{
    public interface IState
    {
        /// <summary>
        /// Gets the table for the current game.
        /// </summary>
        Table Table { get; }

        /// <summary>
        /// Gets all the attributes of the state.
        /// </summary>
        ImmutableList<Attribute> Attributes { get; }
    }

    /// <summary>
    /// Defines the state with a set of attributes/values.
    /// </summary>
    public interface IState<TState>  : IState
        where TState : IState<TState>
    {
    }

    public record StateBase<TState>(
        Table Table,
        ImmutableList<Attribute> Attributes
    )
    : IState<TState>
        where TState : IState<TState>;

    public static class StateExtensions
    {
        #region Utilities

        /// <summary>
        /// Aggregates the values of all attributes of a specific type using selector.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to filter by.</typeparam>
        /// <param name="state">The dungeon in which the action takes place.</param>
        /// <param name="selector">The selector function to get the value from the attribute.</param>
        /// <returns></returns>
        public static int AggregateAttributes<TAttribute>(this IState state, System.Func<TAttribute, int> selector) =>
            state.Attributes
                .OfType<TAttribute>()
                .Aggregate(0, (total, next) => total + selector(next));

        /// <summary>
        /// Checks if player is winning the game by comparing strength of a player and monster.
        /// </summary>
        /// <param name="state">The dungeon in which the action takes place.</param>
        /// <returns></returns>
        public static bool PlayersAreWinningCombat(this IState state)
        {
            int playerStrength = state.AggregateAttributes<PlayerStrengthBonusAttribute>(x => x.Bonus);
            int mosterStrength = state.AggregateAttributes<MonsterStrengthBonusAttribute>(x => x.Bonus);
            return playerStrength > mosterStrength;
        }

        /// <summary>
        /// Gets an attribute of a specific type.
        /// </summary>
        /// <typeparam name="TAttribute">Type of the attribute.</typeparam>
        /// <returns>The attribute instance.</returns>
        public static TAttribute GetAttribute<TAttribute>(this IState state)
            where TAttribute : Attribute =>
            state.Attributes.OfType<TAttribute>().FirstOrDefault();

        #endregion

        #region Attribute Bonuses

        /// <summary>
        /// Monster strength if any present.
        /// </summary>
        public static int MonsterStrength(this IState state) =>
            state.AggregateAttributes<MonsterStrengthBonusAttribute>(x => x.Bonus);

        /// <summary>
        /// Heroes strength that entered the dungeon.
        /// </summary>
        public static int PlayersStrength(this IState state) =>
            state.AggregateAttributes<PlayerStrengthBonusAttribute>(x => x.Bonus);

        /// <summary>
        /// Bonus to run away from dungeon.
        /// </summary>
        public static int RunAwayBonus(this IState state) =>
            state.AggregateAttributes<RunAwayBonusAttribute>(x => x.Bonus);

        /// <summary>
        /// Levels gained if dungeon is cleared.
        /// </summary>
        public static int RewardLevels(this IState state) =>
            state.AggregateAttributes<RewardLevelsAttribute>(x => x.Bonus);

        /// <summary>
        /// Treasures gained if dungeon is cleared.
        /// </summary>
        public static int RewardTreasures(this IState state) =>
            state.AggregateAttributes<RewardTreasuresAttribute>(x => x.Bonus);

        #endregion
    }
}

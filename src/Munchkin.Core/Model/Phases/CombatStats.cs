using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core.Model.Phases
{
    /// <summary>
    /// Defines the statistics of the combat that is going at the moment based on current table state.
    /// </summary>
    /// <param name="FightingPlayer">The player who is fighting in combat.</param>
    /// <param name="HelpingPlayer">The player who is helping the fighting player in combat.</param>
    /// <param name="Monsters">A set of monster that the players are fighting against.</param>
    /// <param name="MonsterStrength">The total monsters strength (including levels and enhancers).</param>
    /// <param name="PlayersStrength">The total players strength (including levels and enhancers).</param>
    public record CombatStats(
        Player FightingPlayer,
        Player HelpingPlayer,
        IReadOnlyCollection<MonsterCard> Monsters,
        int MonsterStrength,
        int PlayersStrength)
    {
        /// <summary>
        /// Creates the combat statistics based on the table state.
        /// </summary>
        /// <param name="table">The current table state.</param>
        /// <returns>Return the statistic object.</returns>
        public static CombatStats From(Table table)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));

            // NOTE: Gets all the monsters in play.
            var monsters = table.DungeonCards
                .OfType<MonsterCard>()
                .ToArray();

            // NOTE: Gets all the monster enhancers in the play.
            var monsterEnhancers = table.DungeonCards
                .Where(x => x.HasAttribute<StrengthBonusAttribute>() || x.HasAttribute<MonsterStrengthBonusAttribute>())
                .Where(x => x.BoundTo is MonsterCard)
                .ToArray();

            var askingForHelp = AskingForHelp.From(table);

            var fightingPlayer = table.Players.Current;
            var helpingPlayer = askingForHelp.HelpingPlayer;

            // NOTE: Gets all the player enhancers in the play.
            var playerEnhancers = table.DungeonCards
                .Where(x => x.HasAttribute<StrengthBonusAttribute>())
                .Where(x => x.GetAttribute<StrengthBonusAttribute>().Bonus > 0)
                .Where(x => x.BoundTo is null)
                .Where(x => x.Owner is not null)
                .Where(x => x.Owner == fightingPlayer || x.Owner == helpingPlayer)
                .ToArray();

            var monsterStrength = GetMonstersStrength(monsters, monsterEnhancers);
            var playersStrength = GetPlayersStrength(fightingPlayer, helpingPlayer, playerEnhancers);

            return new CombatStats(
                fightingPlayer,
                helpingPlayer,
                monsters,
                monsterStrength,
                playersStrength);
        }

        /// <summary>
        /// Gets the monsters in combat strength combined.
        /// </summary>
        /// <returns>An integer value that indicates the strength.</returns>
        private static int GetMonstersStrength(
            IReadOnlyCollection<MonsterCard> Monsters,
            IReadOnlyCollection<Card> MonsterEnhancers)
        {
            var monsterEnhancersStrength = MonsterEnhancers.Aggregate(0, (total, card) =>
                total + card.AggregateAttributes<StrengthBonusAttribute>(x => x.Bonus));
            var monsterLevelsStrength = Monsters.Aggregate(0, (totalStrength, monster) => totalStrength + monster.Level);
            return monsterLevelsStrength + monsterEnhancersStrength;
        }

        /// <summary>
        /// Gets the players in combat strength combined.
        /// </summary>
        /// <returns>An integer value that indicates the strength.</returns>
        private static int GetPlayersStrength(
            Player FightingPlayer,
            Player HelpingPlayer,
            IReadOnlyCollection<Card> PlayerEnhancers)
        {
            var playerEnhancersStrength = PlayerEnhancers.Aggregate(0, (total, card) =>
                total + card.AggregateAttributes<StrengthBonusAttribute>(x => x.Bonus));
            var playerLevelStrength = FightingPlayer.Level
                + FightingPlayer.Strength
                + (HelpingPlayer?.Level ?? 0)
                + (HelpingPlayer?.Strength ?? 0);
            return playerLevelStrength + playerEnhancersStrength;
        }
    }
}

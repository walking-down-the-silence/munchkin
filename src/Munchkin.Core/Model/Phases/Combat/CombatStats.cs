using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core.Model.Phases
{
    public record CombatStats(
        Player FightingPlayer,
        Player HelpingPlayer,
        IReadOnlyCollection<MonsterCard> Monsters,
        int MonsterStrength,
        int PlayersStrength)
    {
        public static CombatStats From(Table table)
        {
            // NOTE: Gets all the monsters in play.
            var monsters = table.TemporaryPile
                .OfType<MonsterCard>()
                .ToArray();

            // NOTE: Gets all the monster enhancers in the play.
            var monsterEnhancers = table.TemporaryPile
                .Where(x => x.HasAttribute<StrengthBonusAttribute>() || x.HasAttribute<MonsterStrengthBonusAttribute>())
                .Where(x => x.BoundTo is MonsterCard)
                .ToArray();

            // NOTE: Gets all the player enhancers in the play.
            var playerEnhancers = table.TemporaryPile
                .Where(x => x.HasAttribute<StrengthBonusAttribute>())
                .Where(x => x.BoundTo is null)
                .ToArray();

            var fightingPlayer = table.Turns.Current.Player;
            var helpingPlayer = (Player)null;
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
            var playerLevelStrength = FightingPlayer.Level + HelpingPlayer?.Level ?? 0;
            return playerLevelStrength + playerEnhancersStrength;
        }
    }
}

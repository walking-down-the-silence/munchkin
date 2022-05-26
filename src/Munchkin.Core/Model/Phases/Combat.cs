using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Attributes;
using Munchkin.Core.Model.Phases.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core.Model.Phases
{
    /// <summary>
    /// Defines a set of actions available to the player during the combat.
    /// </summary>
    /// <param name="FightingPlayer">The player who is fighting in combat.</param>
    /// <param name="HelpingPlayer">The player who is helping the fighting player in combat.</param>
    /// <param name="Monsters">A set of monster that the players are fighting against.</param>
    /// <param name="MonsterStrength">The total monsters strength (including levels and enhancers).</param>
    /// <param name="PlayersStrength">The total players strength (including levels and enhancers).</param>
    public record Combat(
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
        public static Combat From(Table table)
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
                .Where(x => x.BoundTo is null)
                .Where(x => x.Owner is not null)
                .Where(x => x.Owner == fightingPlayer || x.Owner == helpingPlayer)
                .ToArray();

            var monsterStrength = GetMonstersStrength(table, monsters, monsterEnhancers);
            var playersStrength = GetPlayersStrength(table, fightingPlayer, helpingPlayer, playerEnhancers);

            return new Combat(
                fightingPlayer,
                helpingPlayer,
                monsters,
                monsterStrength,
                playersStrength);
        }

        /// <summary>
        /// Gathers the rewards from the battle and distributes them between the fighting player and the helping player.
        /// </summary>
        /// <param name="table">The table state where the game takes place.</param>
        /// <returns>Returns the updated table instance.</returns>
        public static Table Reward(Table table)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));

            var combatStats = Combat.From(table);

            var monsters = table.DungeonCards.OfType<MonsterCard>();
            var rewardTreasures = monsters.Sum(monster => monster.RewardTreasures);
            var rewardLevels = monsters.Sum(monster => monster.RewardLevels);

            combatStats.FightingPlayer.LevelUp(rewardLevels);

            // NOTE: Only Elves go up a level when helping in combat.
            if (combatStats.HelpingPlayer?.HasActiveAttribute<ElfAttribute>() ?? false)
                combatStats.HelpingPlayer?.LevelUp(monsters.Count());

            // TODO: think of a way to distribute treasures based on help agreement
            table = table with { TreasureCardDeck = table.TreasureCardDeck.TakeRange(rewardTreasures, out var treasures) };
            treasures.ForEach(card => combatStats.FightingPlayer.TakeInHand(card));

            return table;
        }

        /// <summary>
        /// Sets the running away states for each player from each monster from combat.
        /// </summary>
        /// <param name="table">The table state where the game takes place.</param>
        /// <returns>Returns the updated table instance.</returns>
        public static Table RunAway(Table table)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));

            var combatStats = Combat.From(table);

            // TODO: table instance should be updated
            // TODO: should be called for all monsters per each player (fighting and helping)
            _ = table.DungeonCards
                .OfType<MonsterCard>()
                .SelectMany(monster => new[]
                {
                    new RunningAwayFromMonsterEvent(combatStats.FightingPlayer.Nickname, monster.GetHashCode().ToString()),
                    new RunningAwayFromMonsterEvent(combatStats.HelpingPlayer.Nickname, monster.GetHashCode().ToString())
                })
                .Aggregate(table, (result, item) => result.WithActionEvent(item));

            return table;
        }

        /// <summary>
        /// Sends a request for rhelp to a selected player.
        /// TODO: send a request for help to the target player
        /// </summary>
        /// <param name="table">The table state where the game takes place.</param>
        /// <param name="targetPlayer">The player who was asked to help in combat.</param>
        /// <returns>Returns the updated table instance.</returns>
        public static Table AskForHelp(Table table, Player targetPlayer)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(targetPlayer, nameof(targetPlayer));

            var askingForHelpEvent = new AskingForHelpPlayerEvent(targetPlayer.Nickname);
            table = table.WithActionEvent(askingForHelpEvent);

            return table;
        }

        /// <summary>
        /// Accepts request for help.
        /// </summary>
        /// <param name="table">The table state where the game takes place.</param>
        /// <param name="askedPlayer">The player who accpeted the help request.</param>
        /// <returns>Returns the updated table instance.</returns>
        public static Table AcceptHelpRequest(Table table, Player askedPlayer)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(askedPlayer, nameof(askedPlayer));

            var askingForHelpEvent = new AskingForHelpAcceptedEvent(askedPlayer.Nickname);
            table = table.WithActionEvent(askingForHelpEvent);

            return table;
        }

        /// <summary>
        /// Rejects rerquest for help.
        /// TODO: remove player that rejected from the list of players to ask
        /// </summary>
        /// <param name="table">The table state where the game takes place.</param>
        /// <param name="askedPlayer">The player who rejected the help request.</param>
        /// <returns>Returns the updated table instance.</returns>
        public static Table RejectHelpRequest(Table table, Player askedPlayer)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(askedPlayer, nameof(askedPlayer));

            var askingForHelpEvent = new AskingForHelpRejectedEvent(askedPlayer.Nickname);
            table = table.WithActionEvent(askingForHelpEvent);

            return table;
        }

        /// <summary>
        /// Gets the monsters in combat strength combined.
        /// </summary>
        /// <returns>An integer value that indicates the strength.</returns>
        private static int GetMonstersStrength(
            Table table,
            IReadOnlyCollection<MonsterCard> monsters,
            IReadOnlyCollection<Card> monsterEnhancers)
        {
            var monsterEnhancersStrength = monsterEnhancers.Sum(card => card.UniversalStrength());
            var monsterLevelsStrength = monsters.Sum(monster => monster.Level);

            var monsterEffectStrength = table.ActionLog
                .OfType<ISupportAttributes>()
                .Sum(x => x.MonsterStrength());

            return monsterLevelsStrength + monsterEnhancersStrength + monsterEffectStrength;
        }

        /// <summary>
        /// Gets the players in combat x combined.
        /// </summary>
        /// <returns>An integer value that indicates the x.</returns>
        private static int GetPlayersStrength(
            Table table,
            Player fightingPlayer,
            Player helpingPlayer,
            IReadOnlyCollection<Card> playerEnhancers)
        {
            var playerEnhancersStrength = playerEnhancers.Sum(card => card.UniversalStrength());

            var playerLevelsStrength = fightingPlayer.Level
                + fightingPlayer.Strength
                + (helpingPlayer?.Level ?? 0)
                + (helpingPlayer?.Strength ?? 0);

            var playerEffectStrength = table.ActionLog
                .OfType<ISupportAttributes>()
                .Sum(x => x.PlayersStrength());

            return playerLevelsStrength + playerEnhancersStrength + playerEffectStrength;
        }
    }
}

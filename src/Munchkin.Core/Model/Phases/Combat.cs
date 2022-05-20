using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Phases.Events;
using System;
using System.Linq;

namespace Munchkin.Core.Model.Phases
{
    /// <summary>
    /// Defines a set of actions available to the player during the combat.
    /// </summary>
    public static class Combat
    {
        /// <summary>
        /// Gathers the rewards from the battle and distributes them between the fighting player and the helping player.
        /// </summary>
        /// <param name="table">The table state where the game takes place.</param>
        /// <returns>Returns the updated table instance.</returns>
        public static Table Reward(Table table)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));

            var combatStats = CombatStats.From(table);

            var monsters = table.DungeonCards.OfType<MonsterCard>();
            var rewardTreasures = monsters.Aggregate(0, (total, monster) => total + monster.RewardTreasures);
            var rewardLevels = monsters.Aggregate(0, (total, monster) => total + monster.RewardLevels);

            // TODO: Check if the helping player receives the levels
            combatStats.FightingPlayer.LevelUp(rewardLevels);
            combatStats.HelpingPlayer?.LevelUp(rewardLevels);

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

            var combatStats = CombatStats.From(table);

            // TODO: table instance should be updated
            // TODO: should be called for all monsters per each player (fighting and helping)
            _ = table.DungeonCards
                .OfType<MonsterCard>()
                .SelectMany(monster => new[]
                {
                    new RunningAwayFromMonsterEvent(combatStats.FightingPlayer.Nickname, monster.GetHashCode().ToString()),
                    new RunningAwayFromMonsterEvent(combatStats.HelpingPlayer.Nickname, monster.GetHashCode().ToString())
                })
                .Aggregate(table.ActionLog, (actionLog, item) => { actionLog.Add(item); return actionLog; });

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
            table.ActionLog.Add(askingForHelpEvent);

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
            table.ActionLog.Add(askingForHelpEvent);

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
            table.ActionLog.Add(askingForHelpEvent);

            return table;
        }
    }
}

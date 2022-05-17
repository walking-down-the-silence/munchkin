using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using System.Linq;

namespace Munchkin.Core.Model.Phases
{
    public static class Combat
    {
        /// <summary>
        /// TODO: run charity for helping player as well
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static Table Reward(Table table)
        {
            var combatStats = CombatStats.From(table);

            // TODO: collect all the treasures, levels and other stuff after a combat
            // TODO: do the same for helping player on agreed treasures
            var monsters = table.TemporaryPile.OfType<MonsterCard>();
            var rewardTreasures = monsters.Aggregate(0, (total, monster) => total + monster.RewardTreasures);
            var rewardLevels = monsters.Aggregate(0, (total, monster) => total + monster.RewardLevels);

            combatStats.FightingPlayer.LevelUp(rewardLevels);
            combatStats.HelpingPlayer?.LevelUp(rewardLevels);

            // TODO: think of a way to distribute treasures based on help agreement
            table.TreasureCardDeck.Take(rewardTreasures)
                .ForEach(card => combatStats.FightingPlayer.TakeInHand(card));

            return table;
        }

        /// <summary>
        /// TODO: introduce the higher-level type for runnniw away for each player from each monster
        /// </summary>
        /// <param name="state"></param>
        /// <param name="monster"></param>
        /// <returns></returns>
        public static Table RunAway(Table table)
        {
            var combatStats = CombatStats.From(table);

            // TODO: table instance should be updated
            // TODO: should be called for all monsters per each player (fighting and helping)
            _ = table.TemporaryPile
                .OfType<MonsterCard>()
                .SelectMany(monster => new[]
                {
                    new RunningAwayFromMonsterEvent(combatStats.FightingPlayer.Nickname, monster.GetHashCode().ToString()),
                    new RunningAwayFromMonsterEvent(combatStats.HelpingPlayer.Nickname, monster.GetHashCode().ToString())
                })
                .Aggregate(table.ActionLog, (actionLog, item) => actionLog.Push(item));

            return table;
        }

        public record RunningAwayFromMonsterEvent(string PlayerNickname, string MonsterCardId);

        /// <summary>
        /// Sends a request for rhelp to a selected player.
        /// TODO: send a request for help to the target player
        /// </summary>
        /// <param name="state"></param>
        /// <param name="targetPlayer"></param>
        /// <returns></returns>
        public static Table AskForHelp(Table table, Player targetPlayer)
        {
            var askingForHelpEvent = new AskingForHelpPlayerEvent(targetPlayer.Nickname);
            table.ActionLog.Push(askingForHelpEvent);

            //var nextState = state with
            //{
            //    HelpingPlayer = null,
            //    AskingForHelp = state.AskingForHelp.WithAskedPlayer(targetPlayer)
            //};
            return table;
        }

        /// <summary>
        /// Accepts request for help.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="askedPlayer"></param>
        /// <returns></returns>
        public static Table AcceptHelpRequest(Table table, Player askedPlayer)
        {
            var askingForHelpEvent = new AskingForHelpAcceptedEvent(askedPlayer.Nickname);
            table.ActionLog.Push(askingForHelpEvent);

            //var nextState = state with
            //{
            //    HelpingPlayer = askedPlayer,
            //    AskingForHelp = state.AskingForHelp.WithoutAskedPlayer()
            //};
            return table;
        }

        /// <summary>
        /// Rejects rerquest for help.
        /// TODO: remove player that rejected from the list of players to ask
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static Table RejectHelpRequest(Table table, Player askedPlayer)
        {
            var askingForHelpEvent = new AskingForHelpRejectedEvent(askedPlayer.Nickname);
            table.ActionLog.Push(askingForHelpEvent);

            //var nextState = state with
            //{
            //    HelpingPlayer = null,
            //    AskingForHelp = state.AskingForHelp.WithoutAskedPlayer()
            //};
            return table;
        }
    }

    public record AskingForHelpPlayerEvent(string AskedPlayerNickname);

    public record AskingForHelpAcceptedEvent(string AskedPlayerNickname);

    public record AskingForHelpRejectedEvent(string AskedPlayerNickname);
}

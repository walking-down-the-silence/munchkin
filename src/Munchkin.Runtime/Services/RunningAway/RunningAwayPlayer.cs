using Munchkin.Core.Model;
using Munchkin.Core.Model.Phases.Events;
using System;
using System.Linq;

namespace Munchkin.Runtime.Services
{
    public record RunningAwayPlayer(Player Player, int DiceRoll)
    {
        public static RunningAwayPlayer From(Table table)
        {
            var runningAway = new RunningAwayPlayer(default, -1);

            runningAway = table.ActionLog
                .OfType<IRunningAwayEvent>()
                .OrderBy(x => x.CreatedDate)
                .Aggregate(runningAway, (result, @event) => Apply(table, result, @event));

            return runningAway;
        }

        private static RunningAwayPlayer Apply(Table table, RunningAwayPlayer runningAway, IRunningAwayEvent runningAwayEvent)
        {
            return runningAwayEvent switch
            {
                RunningAwayFromMonsterEvent event1 => Apply(table, runningAway, event1),
                RunningAwayFromMonsterDiceRollEvent event2 => Apply(table, runningAway, event2),
                _ => throw new ArgumentOutOfRangeException(nameof(runningAwayEvent))
            };
        }

        private static RunningAwayPlayer Apply(Table table, RunningAwayPlayer runningAway, RunningAwayFromMonsterEvent runningAwayEvent)
        {
            var player = table.Players.FirstOrDefault(x => x.Nickname == runningAwayEvent.PlayerNickname);
            return runningAway with
            {
                Player = player,
                DiceRoll = -1
            };
        }

        private static RunningAwayPlayer Apply(Table table, RunningAwayPlayer runningAway, RunningAwayFromMonsterDiceRollEvent diceRollEvent)
        {
            return runningAway with
            {
                DiceRoll = diceRollEvent.DiceRollResult
            };
        }
    }
}

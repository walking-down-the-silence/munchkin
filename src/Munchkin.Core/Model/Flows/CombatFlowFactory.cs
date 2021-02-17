using Munchkin.Core.Model;
using System;

namespace Munchkin.Console
{
    public class CombatFlowFactory : IFlowFactory<CombatStage>
    {
        public IFlowContext<CombatStage> Create()
        {
            var running = new RunAwayFlowFactory().Create();

            //var flow = Flow
            //    .Execute<CombatStage>(state => state.PromptUserToPlayCards())
            //    .Loop(
            //        state => state.AnyCardsPlayed(),
            //        state => state
            //            .Execute(state => state.Recalculate())
            //            .Execute(state => state.PromptUserToPlayCards())
            //    )
            //    .Condition(
            //        state => state.PlayersAreWinningCombat(),
            //        positive => positive.Execute(state => state.TakeGoodStuff()),
            //        negative => negative.Execute(state => running.Build().Invoke(state.Dungeon.RunAway).Dungeon.Combat)
            //     );

            //return flow;

            throw new NotImplementedException();
        }

        public static CombatStage CombatFromRunawayState(RunAwayStage state)
        {
            throw new NotImplementedException();
        }

        public static RunAwayStage RunAwayFromCombatState(CombatStage state)
        {
            throw new NotImplementedException();
        }
    }
}

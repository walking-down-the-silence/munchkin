using Munchkin.Core.Model;
using System;

namespace Munchkin.Console
{
    public class DungeonFlowFactory : IFlowFactory<Dungeon>
    {
        public IFlowContext<Dungeon> Create()
        {
            var battle = new CombatFlowFactory().Create();
            var curse = new CurseFlowFactory().Create();

            var flow = Flow
                //.Execute<DungeonStage>(state => DungeonStage.KickOpenTheDoor(null, null))
                .Condition<Dungeon>(
                    state => state.LastCardPlayedIsBeast,
                    positive => positive.Execute(state => battle.Build().Invoke(state.Combat).Dungeon),
                    negative => negative.Execute(state => curse.Build().Invoke(state.Curse).Dungeon)
                );

            return flow;
        }

        public static Dungeon TurnFromBattleState(CombatStage state)
        {
            // TODO: pass the context from previous instance here
            throw new NotImplementedException();
        }

        public static Dungeon TurnFromCurseState(CurseStage state)
        {
            // TODO: pass the context from previous instance here
            throw new NotImplementedException();
        }

        public static CombatStage CombatFromTurnState(Dungeon state)
        {
            throw new NotImplementedException();
        }

        public static CurseStage CurseFromTurnState(Dungeon state)
        {
            throw new NotImplementedException();
        }
    }
}

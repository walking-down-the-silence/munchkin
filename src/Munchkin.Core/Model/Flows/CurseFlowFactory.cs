using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Console
{
    public class CurseFlowFactory : IFlowFactory<CurseStage>
    {
        public IFlowContext<CurseStage> Create()
        {
            var battle = new CombatFlowFactory().Create();

            var flow = Flow
                .Condition<CurseStage>(
                    state => state.LastPlayedIsCurse(),
                    positive => positive
                        .Loop(
                            state => state.AnyCardsPlayed(),
                            state => state
                                .Execute(state => state.Recalculate())
                                .Execute(state => state.PromptUserToPlayCards())
                        )
                        .Condition(
                            state => state.IsCurseCancelled(),
                            positive => positive,
                            negative => negative.Execute(state => state.TakeBadStuff())
                        ),
                    negative => negative.Execute(state => state.TakeCardInHand())
                )
                .Execute(state => state.PromptUserToPlayBeast())
                .Condition(
                    state => state.IsBeastPlayedFromHand(),
                    positive => positive.Execute(state => battle.Build().Invoke(state.Dungeon.Combat).Dungeon.Curse),
                    negative => negative.Execute(state => state.LootTheRoom())
                );

            return flow;
        }

        public static CurseStage CurseFromBattleState(CombatStage state)
        {
            return CurseStage.FromCurse(state.Dungeon, state.LastCardPlayed as CurseCard);
        }

        public static CombatStage FromNonBattleState(CurseStage state)
        {
            return CombatStage.EnterCombat(state.Dungeon, state.LastCardPlayed as MonsterCard);
        }
    }
}

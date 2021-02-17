using Munchkin.Core.Model;

namespace Munchkin.Console
{
    public class RunAwayFlowFactory : IFlowFactory<RunAwayStage>
    {
        public IFlowContext<RunAwayStage> Create()
        {
            var flow = Flow
                .Condition<RunAwayStage>(
                    state => state.IsRunningAway(),
                    positive => positive
                        .Execute(state => state.RollTheDice())
                        .Execute(state => state.PromptUserToPlayCards())
                        .Loop(
                            state => state.AnyCardsPlayed(),
                            state => state
                                .Execute(state => state.Recalculate())
                                .Execute(state => state.PromptUserToPlayCards())
                        )
                        .Condition(
                            state => state.IsDiceRollSuccessful(),
                            positive => positive.Execute(state => state),
                            negative => negative.Execute(state => state.TakeBadStuff())
                        ),
                    negative => negative.Execute(state => state.TakeBadStuff())
                );

            return flow;
        }
    }
}

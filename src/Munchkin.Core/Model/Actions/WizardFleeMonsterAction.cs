using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Model.Requests;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Actions
{
    internal class WizardFleeMonsterAction : DynamicAction
    {
        public WizardFleeMonsterAction() : base("Flee Monster", "")
        {
        }

        public override bool CanExecute(Table state)
        {
            return state.Players.Current.YourHand.Count >= 3;
        }

        public override async Task<Table> ExecuteAsync(Table state)
        {
            var selectCardFromHandRequest = new SelectCardsRequest(state.Players.Current, state, state.Players.Current.YourHand);
            //await state.RequestSink.Send(selectCardFromHandRequest).ContinueWith(x => x.Result.Discard(state));
            //await state.RequestSink.Send(selectCardFromHandRequest).ContinueWith(x => x.Result.Discard(state));
            //await state.RequestSink.Send(selectCardFromHandRequest).ContinueWith(x => x.Result.Discard(state));

            // TODO: check if current stage actually is a combat
            // TODO: do not actually discard, but remove reward levels and leave the treasures until combat is resolved
            var selectMonsterInPlayRequest = new SelectCardsRequest(state.Players.Current, state, state.Players.Current.YourHand);
            //await state.RequestSink.Send(selectMonsterInPlayRequest).ContinueWith(x => x.Result.Discard(state));

            return state;
        }
    }
}
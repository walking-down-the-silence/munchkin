using Munchkin.Core.Contracts.Actions;
using System;
using System.Threading.Tasks;
using static Munchkin.Core.Model.Cards.MunchkinDeluxeCards;

namespace Munchkin.Core.Model.Actions
{
    internal record WizardCharmSpellAction() :
        DynamicAction(WizardClass.CharmSpell, "Charm Spell", "Flee Monster From Combat"),
        IRenewableAction<Table>
    {
        private bool _wasExecuted = false;

        public bool Reset(Table state)
        {
            _wasExecuted = false;
            return true;
        }

        protected override bool OnCanExecute(Table table)
        {
            return !_wasExecuted && table.Players.Current.YourHand.Count >= 3;
        }

        protected override Task<Table> OnExecuteAsync(Table table)
        {
            //var selectCardFromHandRequest = new PlayerSelectSingleCardRequest(table.Players.Current, table, table.Players.Current.YourHand);
            //await state.RequestSink.Send(selectCardFromHandRequest).ContinueWith(x => x.Result.Discard(state));
            //await state.RequestSink.Send(selectCardFromHandRequest).ContinueWith(x => x.Result.Discard(state));
            //await state.RequestSink.Send(selectCardFromHandRequest).ContinueWith(x => x.Result.Discard(state));

            // TODO: check if current stage actually is a combat
            // TODO: do not actually discard, but remove reward levels and leave the treasures until combat is resolved
            //var selectMonsterInPlayRequest = new PlayerSelectSingleCardRequest(table.Players.Current, table, table.Players.Current.YourHand);
            //await state.RequestSink.Send(selectMonsterInPlayRequest).ContinueWith(x => x.Result.Discard(state));

            throw new NotImplementedException();
        }
    }
}
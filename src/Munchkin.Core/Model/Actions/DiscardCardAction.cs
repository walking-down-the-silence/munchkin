using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Requests;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Actions
{
    /// <summary>
    /// Discards a card
    /// </summary>
    public class DiscardCardAction : DynamicAction
    {
        public DiscardCardAction() : base("Discard Card", "")
        {
        }

        public override bool CanExecute(Table state)
        {
            throw new NotImplementedException();
        }

        public override async Task<Table> ExecuteAsync(Table state)
        {
            //var selectCardRequest = new SelectCardsRequest(state.Players.Current, state, state.Players.Current.AllCards());
            //await state.RequestSink.Send(selectCardRequest).ContinueWith(x => x.Result.Discard(state));

            return state;
        }
    }
}
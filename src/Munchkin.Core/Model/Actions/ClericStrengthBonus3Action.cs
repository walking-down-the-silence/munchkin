using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Requests;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Actions
{
    internal class ClericStrengthBonus3Action : MultiShotAction, IRenewableAction<Table>
    {
        public ClericStrengthBonus3Action() : base(3, "Bonus +3", "")
        {
        }

        public override bool CanExecute(Table state)
        {
            // TODO: check if current stage actually is a combat
            return ExecutionsLeft > 0
                //&& state.Dungeon.Combat.Monsters.Any(monster => monster.IsUndead)
                && (state.Players.Current.Equipped.Any()
                || state.Players.Current.Backpack.Any()
                || state.Players.Current.YourHand.Any());
        }

        public override async Task<Table> ExecuteAsync(Table state)
        {
            // TODO: decrese the ExecutionsLeft counter
            var selectCardRequest = new PlayerSelectSingleCardRequest(state.Players.Current, state, state.Players.Current.AllCards());
            // TODO: add a bonus of +3 for each card
            //await state.RequestSink.Send(selectCardRequest).ContinueWith(x => x.Result.Discard(state));

            return state;
        }

        public bool Reset(Table state)
        {
            throw new System.NotImplementedException();
        }
    }
}

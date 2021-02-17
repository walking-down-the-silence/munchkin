using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Actions;
using Munchkin.Core.Model.Requests;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Actions
{
    internal class ClericStrengthBonus3Action : DynamicAction
    {
        /// <summary>
        /// Gets how many timer per turn and action can be executed
        /// </summary>
        private int _shotsLeft = 3;

        public ClericStrengthBonus3Action() : base("Bonus +3", "")
        {
        }

        public override bool CanExecute(Table state)
        {
            // TODO: check if current stage actually is a combat
            return _shotsLeft > 0
                //&& state.Dungeon.Combat.Monsters.Any(monster => monster.IsUndead)
                && (state.Players.Current.Equipped.Any()
                || state.Players.Current.Backpack.Any()
                || state.Players.Current.YourHand.Any());
        }

        public override async Task<Table> ExecuteAsync(Table state)
        {
            Interlocked.Decrement(ref _shotsLeft);
            var selectCardRequest = new SelectCardRequest(state.Players.Current, state, state.Players.Current.AllCards());
            // TODO: add a bonus of +3 for each card
            await state.Mediator.Send(selectCardRequest).ContinueWith(x => x.Result.Discard(state));

            return state;
        }
    }
}

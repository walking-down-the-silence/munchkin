using Munchkin.Core.Contracts.Actions;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Actions
{
    internal record ClericStrengthBonus3Action() :
        MultiShotAction(string.Empty, "Bonus +3", string.Empty, 3),
        IRenewableAction<Table>
    {
        public override bool CanExecute(Table state)
        {
            // TODO: check if current stage actually is a combat
            return ExecutionsLeft > 0
                //&& state.Dungeon.Combat.Monsters.Any(monster => monster.IsUndead)
                && (state.Players.Current.Equipped.Any()
                || state.Players.Current.Backpack.Any()
                || state.Players.Current.YourHand.Any());
        }

        public override async Task<Table> ExecuteAsync(Table table)
        {
            table = await base.ExecuteAsync(table);

            return table;
        }

        public bool Reset(Table state)
        {
            // TODO: reset the execution count
            throw new System.NotImplementedException();
        }
    }
}

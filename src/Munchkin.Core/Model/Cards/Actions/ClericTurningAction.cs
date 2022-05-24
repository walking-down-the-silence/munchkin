using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Model.Cards.Events;
using System.Linq;
using System.Threading.Tasks;
using static Munchkin.Core.Model.Cards.MunchkinDeluxeCards;

namespace Munchkin.Core.Model.Actions
{
    internal record ClericTurningAction() :
        MultiShotAction(ClericClass.Turning, "Turning", "Bonus +3", 3),
        IRenewableAction<Table>
    {
        public bool Reset(Table state)
        {
            // TODO: reset the execution count
            throw new System.NotImplementedException();
        }

        protected override bool OnCanExecute(Table state)
        {
            // TODO: check if current stage actually is a combat
            return ExecutionsLeft > 0
                //&& state.Dungeon.Combat.Monsters.Any(monster => monster.IsUndead)
                && (state.Players.Current.Equipped.Any()
                || state.Players.Current.Backpack.Any()
                || state.Players.Current.YourHand.Any());
        }

        protected override async Task<Table> OnExecuteAsync(Table table)
        {
            table = await base.OnExecuteAsync(table);

            var clericBonus3Event = new ClericTurningActionEvent(table.Players.Current.Nickname, string.Empty);
            table.ActionLog.Add(clericBonus3Event);

            return table;
        }
    }
}

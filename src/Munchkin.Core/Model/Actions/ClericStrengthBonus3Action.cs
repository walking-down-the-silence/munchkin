using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Attributes;
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

        public override async Task<Table> ExecuteAsync(Table table)
        {
            table = await base.ExecuteAsync(table);

            var allPlayerCards = table.Players.Current.AllCards();

            // TODO: change the request to be able to select MAXIMUM of N cards, but not EXACTLY N
            var selectedCards = await new PlayerSelectMultipleCardsRequest(table.Players.Current, table, allPlayerCards, 3)
                .SendAsync(table);

            // NOTE: adds a bonus of +3 for each card
            //selectedCards.ForEach(card => table.Dungeon.AddAtribute(new PlayerStrengthBonusAttribute(3)));
            selectedCards.DiscardAll(table);

            return table;
        }

        public bool Reset(Table state)
        {
            // TODO: reset the execution count
            throw new System.NotImplementedException();
        }
    }
}

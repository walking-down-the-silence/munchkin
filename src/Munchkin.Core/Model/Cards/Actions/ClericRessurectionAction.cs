using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Model.Cards.Events;
using System.Threading.Tasks;
using static Munchkin.Core.Model.Cards.MunchkinDeluxeCards;

namespace Munchkin.Core.Model.Actions
{
    internal record ClericRessurectionAction(Player player) :
        MultiShotAction(ClericClass.Ressurection, "Ressurection", "Take From Discard", int.MaxValue)
    {
        private bool _wasExecuted = false;

        public bool Reset(Table table)
        {
            _wasExecuted = false;
            return true;
        }

        protected override bool OnCanExecute(Table table) => !_wasExecuted;

        protected override async Task<Table> OnExecuteAsync(Table table)
        {
            table = await base.OnExecuteAsync(table);

            var clericCardRevivedEvent = new ClericClassCardRevivedEvent(table.Players.Current.Nickname, string.Empty);
            table.ActionLog.Add(clericCardRevivedEvent);

            // TODO: think how to pass the respective deck to take the card from it
            return table;
        }
    }
}
using Munchkin.Core.Contracts.Actions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Actions
{
    internal record ClericReviveCardAction(Player player) :
        MultiShotAction(string.Empty, "Revive Card", string.Empty, int.MaxValue)
    {
        private bool _wasExecuted = false;

        public override bool CanExecute(Table state) => !_wasExecuted;

        public override async Task<Table> ExecuteAsync(Table table)
        {
            table = await base.ExecuteAsync(table);

            // TODO: think how to pass the respective deck to take the card from it
            return table;
        }

        public bool Reset(Table table)
        {
            _wasExecuted = false;
            return true;
        }
    }
}
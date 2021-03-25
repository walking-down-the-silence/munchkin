using Munchkin.Core.Contracts.Actions;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Actions
{
    internal class ClericReviveCardAction : MultiShotAction
    {
        private readonly Player _player;
        private bool _wasExecuted = false;

        public ClericReviveCardAction(Player player) : base(int.MaxValue, "Revive Card", "")
        {
            _player = player ?? throw new ArgumentNullException(nameof(player));
        }

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
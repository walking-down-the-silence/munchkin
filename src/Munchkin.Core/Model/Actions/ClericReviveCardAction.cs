using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Actions;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Actions
{
    internal class ClericReviveCardAction : DynamicAction, IRenewableAction<Table>
    {
        private bool _wasExecuted = false;

        public ClericReviveCardAction() : base("Revive Card", "")
        {
        }

        public override bool CanExecute(Table state) => !_wasExecuted;

        public override Task<Table> ExecuteAsync(Table state)
        {
            throw new NotImplementedException();
        }

        public bool Reset(Table state)
        {
            _wasExecuted = false;
            return true;
        }
    }
}
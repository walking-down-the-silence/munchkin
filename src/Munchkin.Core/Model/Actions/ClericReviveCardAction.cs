using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Model;
using System;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Actions
{
    internal class ClericReviveCardAction : DynamicAction
    {
        public ClericReviveCardAction() : base("Revive Card", "")
        {
        }

        public override bool CanExecute(Table state)
        {
            throw new NotImplementedException();
        }

        public override Task<Table> ExecuteAsync(Table state)
        {
            throw new NotImplementedException();
        }
    }
}
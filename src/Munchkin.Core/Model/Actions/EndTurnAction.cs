using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Actions
{
    public class EndTurnAction : DynamicAction
    {
        public EndTurnAction() : base("End Turn", "")
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
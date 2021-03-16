using Munchkin.Core.Contracts.Actions;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Actions
{
    /// <summary>
    /// Asks other player to help in battle
    /// </summary>
    public class PlayerAskForHelpAction : MultiShotAction
    {
        public PlayerAskForHelpAction() : base(int.MaxValue, "Ask For Help", "")
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
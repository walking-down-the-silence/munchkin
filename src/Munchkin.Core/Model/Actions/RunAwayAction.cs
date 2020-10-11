using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Actions
{
    /// <summary>
    /// Tries to run away from monster in the dungeon
    /// </summary>
    public class RunAwayAction : DynamicAction
    {
        public RunAwayAction() : base("Try Run Away", "")
        {
        }

        public override bool CanExecute(Table state)
        {
            throw new NotImplementedException();
        }

        public override async Task<Table> ExecuteAsync(Table state)
        {
            return state;
        }
    }
}
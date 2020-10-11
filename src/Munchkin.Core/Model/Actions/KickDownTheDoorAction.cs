using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Actions
{
    /// <summary>
    /// Kicks down the door and enters the dungeon
    /// </summary>
    public class KickDownTheDoorAction : DynamicAction
    {
        public KickDownTheDoorAction() : base("Kick Down The Door", "")
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
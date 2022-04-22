using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Model.Cards.Doors.Classes;
using System.Linq;

namespace Munchkin.Core.Model.Rules
{
    /// <summary>
    /// Check if current player or helping player has a Cleric class
    /// </summary>
    public class HasClericClassRule : IRule<Table>
    {
        public bool Satisfies(Table state)
        {
            // TODO: check if current stage actually is a combat
            return state.Players.Current.Equipped.OfType<ClericClass>().FirstOrDefault() != null;
            //|| state.Dungeon.Combat.HelpingPlayer?.Equipped.OfType<ClericClass>().FirstOrDefault() != null;
        }
    }
}

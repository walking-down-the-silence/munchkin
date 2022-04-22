using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Model.Cards.Doors.Classes;
using System.Linq;

namespace Munchkin.Core.Model.Rules
{
    public class HasWarriorClassRule : IRule<Table>
    {
        public bool Satisfies(Table state)
        {
            // TODO: check if current stage actually is a combat
            return state.Players.Current.Equipped.OfType<WarriorClass>().FirstOrDefault() != null;
            //|| state.Dungeon.Combat.HelpingPlayer?.Equipped.OfType<WarriorClass>().FirstOrDefault() != null;
        }
    }
}

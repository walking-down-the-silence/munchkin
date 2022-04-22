using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Model.Cards.Doors.Races;
using System.Linq;

namespace Munchkin.Core.Model.Rules
{
    public class HasDwarfClassRule : IRule<Table>
    {
        public bool Satisfies(Table state)
        {
            return state.Players.Current.Equipped.OfType<DwarfRace>().FirstOrDefault() != null;
        }
    }
}

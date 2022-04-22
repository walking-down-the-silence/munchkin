using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Model.Cards.Doors.Races;
using System.Linq;

namespace Munchkin.Core.Model.Rules
{
    public class HasHalflingRaceRule : IRule<Table>
    {
        public bool Satisfies(Table state)
        {
            // TODO: check if current stage actually is a combat
            return state.Players.Current.Equipped.OfType<HalflingRace>().FirstOrDefault() != null;
            //|| state.Dungeon.Combat.HelpingPlayer?.Equipped.OfType<HalflingRace>().FirstOrDefault() != null;
        }
    }
}

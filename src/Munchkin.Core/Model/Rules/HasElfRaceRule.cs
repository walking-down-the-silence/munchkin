using Munchkin.Core.Contracts;
using Munchkin.Engine.Original.Doors;
using System.Linq;

namespace Munchkin.Core.Model.Rules
{
    public class HasElfRaceRule : IRule<Table>
    {
        public bool Satisfies(Table state)
        {
            // TODO: check if current stage actually is a combat
            return state.Players.Current.Equipped.OfType<ElfRace>().FirstOrDefault() != null;
            //|| state.Dungeon.Combat.HelpingPlayer?.Equipped.OfType<ElfRace>().FirstOrDefault() != null;
        }
    }
}

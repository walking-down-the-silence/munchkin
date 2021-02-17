using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Engine.Original.Doors;
using System.Linq;

namespace Munchkin.Core.Cards.Rules
{
    public class HasDwarfRaceRule : IRule<Table>
    {
        public bool Satisfies(Table state)
        {
            // TODO: check if current stage actually is a combat
            return state.Players.Current.Equipped.OfType<DwarfRace>().FirstOrDefault() != null;
                //|| state.Dungeon.CurrentStage.HelpingPlayer?.Equipped.OfType<DwarfRace>().FirstOrDefault() != null;
        }
    }
}

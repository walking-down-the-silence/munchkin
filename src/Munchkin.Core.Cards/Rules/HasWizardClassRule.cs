using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Engine.Original.Doors;
using System.Linq;

namespace Munchkin.Core.Cards.Rules
{
    public class HasWizardClassRule : IRule<Table>
    {
        public bool Satisfies(Table state)
        {
            return state.Players.Current.Equipped.OfType<WizardClass>().FirstOrDefault() != null
                || state.Dungeon.Combat.HelpingPlayer?.Equipped.OfType<WizardClass>().FirstOrDefault() != null;
        }
    }
}

using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Engine.Original.Doors;
using System.Linq;

namespace Munchkin.Core.Cards.Rules
{
    /// <summary>
    /// Check if current player or helping player has a Cleric class
    /// </summary>
    public class HasClericClassRule : IRule<Table>
    {
        public bool Satisfies(Table state)
        {
            return state.Players.Current.Equipped.OfType<ClericClass>().FirstOrDefault() != null
                || state.Dungeon.Combat.HelpingPlayer?.Equipped.OfType<ClericClass>().FirstOrDefault() != null;
        }
    }
}

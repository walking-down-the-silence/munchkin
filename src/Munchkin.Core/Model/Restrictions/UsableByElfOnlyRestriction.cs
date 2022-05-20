using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Attributes;
using System.Linq;

namespace Munchkin.Core.Model.Restrictions
{
    public class UsableByElfOnlyRestriction : IRule<Table>
    {
        public bool Satisfies(Table state)
        {
            return state.Players.Current.Equipped.Any(x => x.HasAttribute<ElfAttribute>());
        }
    }
}

using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Rules;

namespace Munchkin.Core.Model.Restrictions
{
    public class UsableByMaleOnlyRestriction : IRule<Table>
    {
        public bool Satisfies(Table state)
        {
            return state.Players.Current.Gender == EGender.Male;
        }
    }
}
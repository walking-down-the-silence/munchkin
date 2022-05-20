using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Rules;

namespace Munchkin.Core.Model.Restrictions
{
    public class UsableByFemaleOnlyRestriction : IRule<Table>
    {
        public bool Satisfies(Table state)
        {
            return state.Players.Current.Gender == EGender.Female;
        }
    }
}
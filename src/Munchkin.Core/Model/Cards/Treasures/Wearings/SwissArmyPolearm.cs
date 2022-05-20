using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Restrictions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class SwissArmyPolearm : WearingCard
    {
        public SwissArmyPolearm() :
            base(MunchkinDeluxeCards.Treasures.SwissArmyPolearm, "Swiss Army Polearm", 4, 0, EItemSize.Big, EWearingType.TwoHanded, 600)
        {
            AddRestriction(new UsableByHumanOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
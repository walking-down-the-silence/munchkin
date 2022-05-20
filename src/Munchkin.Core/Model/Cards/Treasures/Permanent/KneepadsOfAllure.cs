using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Restrictions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class KneepadsOfAllure : PermanentItemCard
    {
        public KneepadsOfAllure() :
            base(MunchkinDeluxeCards.Treasures.KneepadsOfAllure, "Kneepads Of Allure", 0, 0, EItemSize.Small, EWearingType.None, 600)
        {
            AddRestriction(new NotUsableByClericRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
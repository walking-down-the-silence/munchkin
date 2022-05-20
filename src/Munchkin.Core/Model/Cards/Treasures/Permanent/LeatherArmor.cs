using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class LeatherArmor : PermanentItemCard
    {
        public LeatherArmor() :
            base(MunchkinDeluxeCards.Treasures.LeatherArmor, "Leather Armor", 1, 0, EItemSize.Small, EWearingType.Armor, 200)
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
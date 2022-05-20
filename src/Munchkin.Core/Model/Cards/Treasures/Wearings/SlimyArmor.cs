using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class SlimyArmor : WearingCard
    {
        public SlimyArmor() : 
            base(MunchkinDeluxeCards.Treasures.SlimyArmor, "Slimy Armor", 1, 0, EItemSize.Small, EWearingType.Armor, 200)
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
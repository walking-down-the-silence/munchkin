using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class BootsOfButtKicking : PermanentItemCard
    {
        public BootsOfButtKicking() :
            base(MunchkinDeluxeCards.Treasures.BootsOfButtKicking, "Boots Of Butt Kicking", 2, 0, EItemSize.Small, EWearingType.Footgear, 400)
        {
        }

        public override Task Play(Table gameContext)
        {
            throw new System.NotImplementedException();
        }
    }
}
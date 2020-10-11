using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class BootsOfButtKicking : PermanentItemCard
    {
        public BootsOfButtKicking() : base("Boots Of Butt Kicking", 2, 0, EItemSize.Small, EWearingType.Footgear, 400)
        {
        }

        public override Task Play(Table gameContext)
        {
            throw new System.NotImplementedException();
        }
    }
}
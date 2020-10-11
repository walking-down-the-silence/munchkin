using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class FlamingArmor : PermanentItemCard
    {
        public FlamingArmor() : base("FlamingArmor", 2, 0, EItemSize.Small, EWearingType.Armor, 400)
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
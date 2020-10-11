using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class SlimyArmor : PermanentItemCard
    {
        public SlimyArmor() : base("Slimy Armor", 1, 0, EItemSize.Small, EWearingType.Armor, 200)
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
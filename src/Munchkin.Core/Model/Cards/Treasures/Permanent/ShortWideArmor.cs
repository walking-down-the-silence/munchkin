using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using Munchkin.Core.Model.Enums;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class ShortWideArmor : PermanentItemCard
    {
        public ShortWideArmor() : base("Short Wide Armor", 3, 0, EItemSize.Small, EWearingType.Armor, 400)
        {
            AddProperty(new DwarfOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
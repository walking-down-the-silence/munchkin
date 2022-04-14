using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using Munchkin.Core.Model.Enums;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class HammerOfKneecapping : PermanentItemCard
    {
        public HammerOfKneecapping() : base("Hammer Of Kneecapping", 4, 0, EItemSize.Small, EWearingType.OneHanded, 600)
        {
            AddProperty(new DwarfOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
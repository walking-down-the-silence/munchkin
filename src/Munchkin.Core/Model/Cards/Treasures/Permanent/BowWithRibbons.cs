using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class BowWithRibbons : PermanentItemCard
    {
        public BowWithRibbons() : base("Bow With Ribbons", 4, 0, EItemSize.Small, EWearingType.TwoHanded, 800)
        {
            AddProperty(new ElfOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;
using Munchkin.Engine.Original.CardProperties;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
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
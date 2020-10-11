using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;
using Munchkin.Engine.Original.CardProperties;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class MithrilArmor : PermanentItemCard
    {
        public MithrilArmor() : base("Mithril Armor", 3, 0, EItemSize.Big, EWearingType.Armor, 600)
        {
            AddProperty(new NotForWizardRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
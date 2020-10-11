using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;
using Munchkin.Engine.Original.CardProperties;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class MaceOfSharpness : PermanentItemCard
    {
        public MaceOfSharpness() : base("Mace Of Sharpness", 4, 0, EItemSize.Small, EWearingType.OneHanded, 600)
        {
            AddProperty(new ClericOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
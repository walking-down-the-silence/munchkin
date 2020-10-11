using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;
using Munchkin.Engine.Original.CardProperties;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class PantyhoseOfGiantStrength : PermanentItemCard
    {
        public PantyhoseOfGiantStrength() : base("Pantyhose Of Giant Strength", 3, 0, EItemSize.Small, EWearingType.None, 600)
        {
            AddProperty(new NotForWarriorRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
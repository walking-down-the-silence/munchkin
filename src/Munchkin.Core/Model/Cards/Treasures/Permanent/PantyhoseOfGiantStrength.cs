using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
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
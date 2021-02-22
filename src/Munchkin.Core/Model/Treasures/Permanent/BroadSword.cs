using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using Munchkin.Core.Model.Enums;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class BroadSword : PermanentItemCard
    {
        public BroadSword() : base("Broad Sword", 3, 0, EItemSize.Small, EWearingType.OneHanded, 400)
        {
            AddProperty(new FemaleOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
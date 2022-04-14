using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using Munchkin.Core.Model.Enums;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class PointyHatOfPower : PermanentItemCard
    {
        public PointyHatOfPower() : base("Pointy Hat Of Power", 3, 0, EItemSize.Small, EWearingType.Headgear, 400)
        {
            AddProperty(new WizardOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
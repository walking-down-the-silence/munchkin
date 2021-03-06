using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using Munchkin.Core.Model.Enums;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class BadAssBandana : PermanentItemCard
    {
        public BadAssBandana() : base("Bad-Ass Bandana", 3, 0, EItemSize.Small, EWearingType.Headgear, 400)
        {
            AddProperty(new HumanOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using Munchkin.Core.Model.Enums;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class LimburgerAndAnchovySandwich : PermanentItemCard
    {
        public LimburgerAndAnchovySandwich() : base("Limburger And Anchovy Sandwich", 3, 0, EItemSize.Small, EWearingType.None, 400)
        {
            AddProperty(new HalflingOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
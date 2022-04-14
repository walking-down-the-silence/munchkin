using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class LeatherArmor : PermanentItemCard
    {
        public LeatherArmor() : base("Leather Armor", 1, 0, EItemSize.Small, EWearingType.Armor, 200)
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
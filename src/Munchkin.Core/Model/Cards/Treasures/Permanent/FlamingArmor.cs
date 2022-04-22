using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class FlamingArmor : PermanentItemCard
    {
        public FlamingArmor() : base("FlamingArmor", 2, 0, EItemSize.Small, EWearingType.Armor, 400)
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
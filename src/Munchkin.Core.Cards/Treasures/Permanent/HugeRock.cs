using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class HugeRock : PermanentItemCard
    {
        public HugeRock() : base("Huge Rock", 3, 0, EItemSize.Big, EWearingType.TwoHanded, 0)
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
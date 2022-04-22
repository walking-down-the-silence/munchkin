using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class SneakyBastardSword : PermanentItemCard
    {
        public SneakyBastardSword() : base("Sneaky Bastard Sword", 2, 0, EItemSize.Small, EWearingType.OneHanded, 400)
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
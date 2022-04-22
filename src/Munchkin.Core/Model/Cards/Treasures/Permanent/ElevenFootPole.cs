using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class ElevenFootPole : PermanentItemCard
    {
        public ElevenFootPole() : base("Eleven Foot Pole", 1, 0, EItemSize.Small, EWearingType.TwoHanded, 200)
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
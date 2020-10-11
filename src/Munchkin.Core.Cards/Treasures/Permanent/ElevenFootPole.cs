using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
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
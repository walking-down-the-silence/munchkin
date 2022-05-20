using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Attributes;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.OneShot
{
    public class MagicLamp : OneShotItemCard
    {
        public MagicLamp() : 
            base(MunchkinDeluxeCards.Treasures.MagicLamp, "Magic Lamp", 0, 0, 500)
        {
            AddAttribute(new NoLevelAttribute());
        }

        public override Task Play(Table gameContext)
        {
            var monster = BoundTo as MonsterCard;
            monster?.Discard(gameContext);
            return base.Play(gameContext);
        }
    }
}
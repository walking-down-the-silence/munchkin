using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Engine.Original.CardProperties;

namespace Munchkin.Engine.Original.Treasures
{
    public class MagicLamp : OneShotItemCard
    {
        public MagicLamp() : base("Magic Lamp", 0, 0, 500)
        {
            AddProperty(new NoLevelAttribute());
        }

        public override Task Play(Table gameContext)
        {
            var monster = BoundTo as MonsterCard;
            monster?.Discard(gameContext);
            return base.Play(gameContext);
        }
    }
}
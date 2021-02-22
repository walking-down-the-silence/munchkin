using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class FriendshipPotion : OneShotItemCard
    {
        public FriendshipPotion() : base("Friendship Potion", 0, 0, 200)
        {
            AddProperty(new NoTreasuresAttribute());
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

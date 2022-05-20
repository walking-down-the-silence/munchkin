using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Attributes;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.OneShot
{
    public sealed class FriendshipPotion : OneShotItemCard
    {
        public FriendshipPotion() :
            base(MunchkinDeluxeCards.Treasures.FriendshipPotion, "Friendship Potion", 0, 0, 200)
        {
            AddAttribute(new NoTreasuresAttribute());
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

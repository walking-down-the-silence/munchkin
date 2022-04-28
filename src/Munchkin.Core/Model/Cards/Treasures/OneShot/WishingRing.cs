using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Attributes;

namespace Munchkin.Core.Model.Cards.Treasures.OneShot
{
    public sealed class WishingRing : OneShotItemCard
    {
        public WishingRing() : base("Wishing Ring", 0, 0, 500)
        {
            AddProperty(new CancelCurseAttribute());
        }
    }
}
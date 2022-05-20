using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Attributes;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.OneShot
{
    public sealed class Doppleganger : OneShotItemCard
    {
        public Doppleganger() :
            base(MunchkinDeluxeCards.Treasures.Doppleganger, "Doppleganger", 0, 0, 300)
        {
        }

        public override Task Play(Table gameContext)
        {
            // take all strength properties from player
            var playerProperties = Owner.Equipped
                .SelectMany(x => x.Attributes)
                .OfType<StrengthBonusAttribute>();

            // take all strength properties from cards used by player
            var oneShotProperties = BoundTo.BoundCards
                .NotOfType<Doppleganger>()
                .SelectMany(x => x.Attributes)
                .OfType<StrengthBonusAttribute>();

            // calculate doppleganger strength
            int strength = oneShotProperties.Select(x => x.Bonus).Aggregate((x, y) => x + y);
            strength += playerProperties.Select(x => x.Bonus).Aggregate((x, y) => x + y);

            // add doppleganger strength
            // TODO: check if current stage actually is a combat
            //gameContext.Dungeon.Combat.AddAttribute(new PlayerStrengthBonusAttribute(strength));
            return base.Play(gameContext);
        }
    }
}
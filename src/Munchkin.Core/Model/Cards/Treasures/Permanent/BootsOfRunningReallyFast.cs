using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class BootsOfRunningReallyFast : PermanentItemCard
    {
        public BootsOfRunningReallyFast() :
            base(MunchkinDeluxeCards.Treasures.BootsOfRunningReallyFast, "Boots Of Running Really Fast", 0, 2, EItemSize.Small, EWearingType.Footgear, 400)
        {
        }

        public override Task Play(Table gameContext)
        {
            // TODO: check if current stage actually is a combat
            //gameContext.Dungeon.Combat.AddAttribute(new RunAwayBonusAttribute(GetAttribute<RunAwayBonusAttribute>().Bonus));
            return base.Play(gameContext);
        }
    }
}
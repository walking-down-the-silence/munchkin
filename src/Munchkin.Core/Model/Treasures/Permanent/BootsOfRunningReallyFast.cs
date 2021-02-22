using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using Munchkin.Core.Model.Properties;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class BootsOfRunningReallyFast : PermanentItemCard
    {
        public BootsOfRunningReallyFast() : base("Boots Of Running Really Fast", 0, 2, EItemSize.Small, EWearingType.Footgear, 400)
        {
        }

        public override Task Play(Table gameContext)
        {
            // TODO: check if current stage actually is a combat
            //gameContext.Dungeon.Combat.AddProperty(new RunAwayBonusAttribute(GetProperty<RunAwayBonusAttribute>().Bonus));
            return base.Play(gameContext);
        }
    }
}
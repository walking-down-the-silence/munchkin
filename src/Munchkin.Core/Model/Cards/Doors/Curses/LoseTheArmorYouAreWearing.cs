using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class LoseTheArmorYouAreWearing : CurseCard
    {
        public LoseTheArmorYouAreWearing() : 
            base(MunchkinDeluxeCards.Doors.LoseTheArmorYouAreWearing, "Lose The Armor You Are Wearing")
        {
        }

        public override Task BadStuff(Table context)
        {
            context.Players.Current.Equipped
                .OfType<WearingCard>()
                .FirstOrDefault(x => x.WearingType == EWearingType.Armor)
                ?.Discard(context);
            return Task.CompletedTask;
        }
    }
}
using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class LoseTheHeadgearYouAreWearing : CurseCard
    {
        public LoseTheHeadgearYouAreWearing() : 
            base(MunchkinDeluxeCards.Doors.LoseTheHeadgearYouAreWearing, "Lose The Headgear You Are Wearing")
        {
        }

        public override Task BadStuff(Table context)
        {
            context.Players.Current.Equipped
                .OfType<PermanentItemCard>()
                .FirstOrDefault(x => x.WearingType == EWearingType.Headgear)
                ?.Discard(context);
            return Task.CompletedTask;
        }
    }
}
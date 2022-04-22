using System.Linq;
using System.Threading.Tasks;
using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class LoseTheFootgearYouAreWearing : CurseCard
    {
        public LoseTheFootgearYouAreWearing() : base("Lose The Footgear You Are Wearing")
        {
        }

        public override Task BadStuff(Table context)
        {
            context.Players.Current.Equipped
                .OfType<PermanentItemCard>()
                .FirstOrDefault(x => x.WearingType == EWearingType.Footgear)
                ?.Discard(context);
            return Task.CompletedTask;
        }
    }
}
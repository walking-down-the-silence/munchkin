using System.Linq;
using System.Threading.Tasks;
using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class LoseTheArmorYouAreWearing : CurseCard
    {
        public LoseTheArmorYouAreWearing() : base("Lose The Armor You Are Wearing")
        {
        }

        public override Task BadStuff(Table context)
        {
            context.Players.Current.Equipped
                .OfType<PermanentItemCard>()
                .FirstOrDefault(x => x.WearingType == EWearingType.Armor)
                ?.Discard(context);
            return Task.CompletedTask;
        }
    }
}
using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class ChangeRace : CurseCard
    {
        public ChangeRace() : base("Change Race")
        {
        }

        public override Task BadStuff(Table context)
        {
            foreach (var equippedCard in context.Players.Current.Equipped)
            {
                if (equippedCard is RaceCard || equippedCard is Halfbreed)
                {
                    equippedCard.Discard(context);
                }
            }

            context = context with { DiscardedDoorsCards = context.DiscardedDoorsCards.TakeFirst<RaceCard>(out var firstDiscardedRace) };
            if (firstDiscardedRace != null)
            {
                context.Players.Current.Equip(firstDiscardedRace);
            }

            // TODO: resolve all other cards that don't match new race
            return Task.CompletedTask;
        }
    }
}
using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

namespace Munchkin.Engine.Original.Doors
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

            var firstDiscardedRace = context.DiscardedDoorsCards.TakeFirst<RaceCard>();
            if (firstDiscardedRace != null)
            {
                context.Players.Current.Equip(firstDiscardedRace);
            }

            // TODO: resolve all other cards that don't match new race
            return Task.CompletedTask;
        }
    }
}
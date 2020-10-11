using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class ChangeRace : CurseCard
    {
        public ChangeRace() : base("Change Race")
        {
        }

        public override Task Play(Table context)
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
                context.Players.Current.PutInPlayAsEquipped(firstDiscardedRace);
            }

            // TODO: resolve all other cards that don't match new race
            return Task.CompletedTask;
        }
    }
}
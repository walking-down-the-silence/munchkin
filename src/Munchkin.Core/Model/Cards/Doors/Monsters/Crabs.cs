using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class Crabs : MonsterCard
    {
        public Crabs() :
            base(MunchkinDeluxeCards.Doors.Crabs, "Crabs", 1, 1, 1, 0, false)
        {
        }

        public override Task BadStuff(Table state)
        {
            state.Players.Current.Equipped
                .OfType<PermanentItemCard>()
                .Where(x => x.WearingType == EWearingType.Armor || x.WearingType == EWearingType.Footgear)
                .ForEach(x => x.Discard(state));

            return Task.CompletedTask;
        }
    }
}
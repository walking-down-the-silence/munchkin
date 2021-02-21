using System.Linq;
using System.Threading.Tasks;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class Crabs : MonsterCard
    {
        public Crabs() : base("Crabs", 1, 1, 1, 0, false)
        {
        }

        public override Task BadStuff(Table state)
        {
            state.Players.Current.Equipped
                .OfType<PermanentItemCard>()
                .Where(x => x.WearingType == EWearingType.Armor && x.WearingType == EWearingType.Footgear)
                .ForEach(x => x.Discard(state));

            return Task.CompletedTask;
        }
    }
}
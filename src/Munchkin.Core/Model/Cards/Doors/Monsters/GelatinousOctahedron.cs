using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class GelatinousOctahedron : MonsterCard
    {
        public GelatinousOctahedron() :
            base(MunchkinDeluxeCards.Doors.GelatinousOctahedron, "Gelatinous Octahedron", 2, 1, 1, 1, false)
        {
        }

        public override Task BadStuff(Table state)
        {
            state.Players.Current.Equipped
                .OfType<ItemCard>()
                .Where(x => x.ItemSize == EItemSize.Big)
                .ForEach(x => x.Discard(state));

            return Task.CompletedTask;
        }
    }
}
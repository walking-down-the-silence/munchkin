using System.Linq;
using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class GelatinousOctahedron : MonsterCard
    {
        public GelatinousOctahedron() : base("Gelatinous Octahedron", 2, 1, 1, 1, false)
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
using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using System;
using System.Linq;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class GelatinousOctahedron : MonsterCard
    {
        public GelatinousOctahedron() :
            base(MunchkinDeluxeCards.Doors.GelatinousOctahedron, "Gelatinous Octahedron", 2, 1, 1, 1, false)
        {
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            player.Equipped
                .OfType<ItemCard>()
                .Where(x => x.ItemSize == EItemSize.Big)
                .ForEach(x => x.Discard(table));

            return table;
        }
    }
}
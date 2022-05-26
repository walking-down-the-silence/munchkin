using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using System;
using System.Linq;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class Crabs : MonsterCard
    {
        public Crabs() :
            base(MunchkinDeluxeCards.Doors.Crabs, "Crabs", 1, 1, 1, 0, false)
        {
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            player.Equipped
                .OfType<WearingCard>()
                .Where(x => x.WearingType == EWearingType.Armor || x.WearingType == EWearingType.Footgear)
                .ForEach(x => x.Discard(table));

            return table;
        }
    }
}
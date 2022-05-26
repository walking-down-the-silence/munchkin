using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System;
using System.Linq;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class LoseTheFootgearYouAreWearing : CurseCard
    {
        public LoseTheFootgearYouAreWearing() :
            base(MunchkinDeluxeCards.Doors.LoseTheFootgearYouAreWearing, "Lose The Footgear You Are Wearing")
        {
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            player.Equipped
                .OfType<WearingCard>()
                .FirstOrDefault(x => x.WearingType == EWearingType.Footgear)
                ?.Discard(table);

            return table;
        }
    }
}
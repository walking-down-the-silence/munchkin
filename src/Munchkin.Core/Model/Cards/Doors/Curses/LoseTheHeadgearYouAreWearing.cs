using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System;
using System.Linq;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class LoseTheHeadgearYouAreWearing : CurseCard
    {
        public LoseTheHeadgearYouAreWearing() :
            base(MunchkinDeluxeCards.Doors.LoseTheHeadgearYouAreWearing, "Lose The Headgear You Are Wearing")
        {
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            player.Equipped
                .OfType<WearingCard>()
                .FirstOrDefault(x => x.WearingType == EWearingType.Headgear)
                ?.Discard(table);

            return table;
        }
    }
}
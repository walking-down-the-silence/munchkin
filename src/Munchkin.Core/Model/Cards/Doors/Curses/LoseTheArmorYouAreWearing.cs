using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System;
using System.Linq;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class LoseTheArmorYouAreWearing : CurseCard
    {
        public LoseTheArmorYouAreWearing() : 
            base(MunchkinDeluxeCards.Doors.LoseTheArmorYouAreWearing, "Lose The Armor You Are Wearing")
        {
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            player.Equipped
                .OfType<WearingCard>()
                .FirstOrDefault(x => x.WearingType == EWearingType.Armor)
                ?.Discard(table);

            return table;
        }
    }
}
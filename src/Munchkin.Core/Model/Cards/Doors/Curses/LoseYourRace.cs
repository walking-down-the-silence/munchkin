using Munchkin.Core.Contracts.Cards;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class LoseYourRace : CurseCard
    {
        public LoseYourRace() :
            base(MunchkinDeluxeCards.Doors.LoseYourRace, "Lose Your Race")
        {
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            var races = player.Equipped
                .OfType<RaceCard>()
                .ToList();

            return races.Count switch
            {
                > 1 => DiscardSelected(table, races),
                1 => DiscardSingle(table, races),
                < 1 => table
            };
        }

        private static Table DiscardSingle(Table table, IReadOnlyCollection<RaceCard> races)
        {
            return table.Discard(races.FirstOrDefault());
        }

        private static Table DiscardSelected(Table table, IReadOnlyCollection<RaceCard> races)
        {
            // TODO: Select which one to discard
            return table;
        }
    }
}
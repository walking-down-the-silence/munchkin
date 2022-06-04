using Munchkin.Core.Contracts.Cards;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class LoseYourClass : CurseCard
    {
        public LoseYourClass() :
            base(MunchkinDeluxeCards.Doors.LoseYourClass, "Lose Your Class")
        {
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            var classes = player.Equipped
                .OfType<ClassCard>()
                .ToList();

            return classes.Count switch
            {
                > 1 => DiscardSelected(table, classes),
                1   => DiscardSingle(table, classes),
                < 1 => table
            };
        }

        private static Table DiscardSingle(Table table, IReadOnlyCollection<ClassCard> classes)
        {
            return table.Discard(classes.FirstOrDefault());
        }

        private static Table DiscardSelected(Table table, IReadOnlyCollection<ClassCard> classes)
        {
            // TODO: Select which one to discard
            return table;
        }
    }
}
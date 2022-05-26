using Munchkin.Core.Contracts.Cards;
using System;
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

            if (classes.Count > 1)
            {
                // select which one to discard
            }
            else
            {
                classes.FirstOrDefault()?.Discard(table);
            }

            return table;
        }
    }
}
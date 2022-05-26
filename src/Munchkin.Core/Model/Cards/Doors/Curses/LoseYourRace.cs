using Munchkin.Core.Contracts.Cards;
using System;
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

            var races = table.Players.Current.Equipped
                .OfType<RaceCard>()
                .ToList();

            if (races.Count > 1)
            {
                // select which one to discard
            }
            else
            {
                races.FirstOrDefault()?.Discard(table);
            }

            return table;
        }
    }
}
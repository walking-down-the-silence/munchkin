using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class ChangeRace : CurseCard
    {
        public ChangeRace() : base(MunchkinDeluxeCards.Doors.ChangeRace, "Change Race")
        {
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            foreach (var equippedCard in table.Players.Current.Equipped)
            {
                if (equippedCard is RaceCard || equippedCard is Halfbreed)
                {
                    equippedCard.Discard(table);
                }
            }

            table = table with { DiscardedDoorsCards = table.DiscardedDoorsCards.TakeFirst<RaceCard>(out var firstDiscardedRace) };
            if (firstDiscardedRace != null)
            {
                table.Players.Current.Equip(firstDiscardedRace);
            }

            // TODO: resolve all other cards that don't match new race
            return table;
        }
    }
}
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using System;
using System.Linq;

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

            player.Equipped
                .Where(x => x is RaceCard || x is Halfbreed)
                .ForEach(x => x.Discard(table));

            table = table with { DiscardedDoorsCards = table.DiscardedDoorsCards.TakeFirst<RaceCard>(out var raceCard) };

            player.Equip(raceCard);
            player.Equipped
                .Where(x => !x.Restrictions.Any(x => x.Satisfies(table)))
                .ForEach(x => player.PutInBackpack(x));

            return table;
        }
    }
}
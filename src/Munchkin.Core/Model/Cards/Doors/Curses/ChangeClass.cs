using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using System;
using System.Linq;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class ChangeClass : CurseCard
    {
        public ChangeClass() : base(MunchkinDeluxeCards.Doors.ChangeClass, "Change Class")
        {
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            player.Equipped
                .Where(x => x is ClassCard || x is SuperMunchkin)
                .ForEach(x => x.Discard(table));

            table = table with { DiscardedDoorsCards = table.DiscardedDoorsCards.TakeFirst<ClassCard>(out var classCard) };

            player.Equip(classCard);
            player.Equipped
                .Where(x => !x.Restrictions.Any(x => x.Satisfies(table)))
                .ForEach(x => player.PutInBackpack(x));

            return table;
        }
    }
}
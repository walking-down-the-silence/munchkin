using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Attributes;
using System;
using System.Linq;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class ChangeSex : CurseCard
    {
        public ChangeSex() :
            base(MunchkinDeluxeCards.Doors.ChangeSex, "Change Sex")
        {
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            // NOTE: Make sure all the items that no longer apply to the rules are taken off
            player.ChangeSex();
            player.Equipped
                .Where(x => x.HasAttribute<GenderAttribute>())
                .Where(x => x.GetAttribute<GenderAttribute>().Gender != player.Gender)
                .ForEach(x => player.PutInBackpack(x));

            return table;
        }
    }
}

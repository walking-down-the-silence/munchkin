using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Attributes;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class ChickenOnYourHead : CurseCard
    {
        public ChickenOnYourHead() :
            base(MunchkinDeluxeCards.Doors.ChickenOnYourHead, "Chiken On Your Head")
        {
            AddAttribute(new RunAwayBonusAttribute(-1));
            AddAttribute(new WearingTypeAttribute(EWearingType.Headgear));
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            Owner.Equip(this);
            table.DungeonCards.Remove(this);

            return table;
        }
    }
}
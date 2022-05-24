using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Attributes;
using System;
using System.Threading.Tasks;

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

        public override Task BadStuff(Table table)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));

            Owner.Equip(this);
            table.DungeonCards.Remove(this);

            return Task.CompletedTask;
        }
    }
}
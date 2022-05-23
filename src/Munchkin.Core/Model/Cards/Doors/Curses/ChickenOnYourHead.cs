using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Attributes;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class ChickenOnYourHead : CurseCard, IEquippable
    {
        public ChickenOnYourHead() :
            base(MunchkinDeluxeCards.Doors.ChickenOnYourHead, "Chiken On Your Head")
        {
            AddAttribute(new RunAwayBonusAttribute(-1));
        }

        public void Equip(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            player.Equip(this);
        }

        public override Task BadStuff(Table table)
        {
            Equip(table, Owner);

            return Task.CompletedTask;
        }
    }
}
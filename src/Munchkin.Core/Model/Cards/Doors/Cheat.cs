using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Attributes;
using Munchkin.Core.Model.Exceptions;
using System;

namespace Munchkin.Core.Model.Cards.Doors
{
    public sealed class Cheat : SpecialCard, IEquippable
    {
        public Cheat() :
            base(MunchkinDeluxeCards.Doors.Cheat, "Cheat")
        {
            AddAttribute(new CheatAttribute());
        }

        public void Equip(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            if (Owner != player)
                throw new PlayerDoesNotOwnTheCardException();

            player.Equip(this);
        }
    }
}
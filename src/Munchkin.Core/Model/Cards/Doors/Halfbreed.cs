using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Attributes;
using Munchkin.Core.Model.Exceptions;
using System;

namespace Munchkin.Core.Model.Cards.Doors
{
    public class Halfbreed : SpecialCard, IEquippable
    {
        public Halfbreed() :
            base(MunchkinDeluxeCards.Doors.Halfbreed1, "Half-breed")
        {
            AddAttribute(new MaximumEquippedRacesAttribute(2));
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
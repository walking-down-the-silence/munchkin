using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class Hippogriff : MonsterCard
    {
        public Hippogriff() :
            base(MunchkinDeluxeCards.Doors.Hippogriff, "Hippogriff", 16, 2, 4, 0, false)
        {
            //TODO: will not persue anyone with level 3 or below
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            throw new System.NotImplementedException();
        }
    }
}
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class PlutoniumDragon : MonsterCard
    {
        public PlutoniumDragon() :
            base(MunchkinDeluxeCards.Doors.PlutoniumDragon, "Plutonium Dragon", 20, 2, 5, 0, false)
        {
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            if (player.Level > 5)
            {
                table.KillPlayer(player);
            }

            return table;
        }
    }
}
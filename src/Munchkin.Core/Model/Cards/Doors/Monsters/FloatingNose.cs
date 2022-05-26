using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class FloatingNose : MonsterCard
    {
        public FloatingNose() :
            base(MunchkinDeluxeCards.Doors.FloatingNose, "Floating Nose", 10, 1, 3, 0, false)
        {
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            table.Players.Current.LevelDown(3);
            return table;
        }
    }
}
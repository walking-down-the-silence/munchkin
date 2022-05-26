using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class Gazebo : MonsterCard
    {
        public Gazebo() :
            base(MunchkinDeluxeCards.Doors.Gazebo, "Gazebo", 8, 1, 2, 0, false)
        {
            //TODO: no one can help you
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
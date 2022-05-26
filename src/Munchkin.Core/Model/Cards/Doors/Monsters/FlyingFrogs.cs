using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class FlyingFrogs : MonsterCard
    {
        public FlyingFrogs() :
            base(MunchkinDeluxeCards.Doors.FlyingFrogs, "Flying Frogs", 2, 1, 1, -1, false)
        {
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            table.Players.Current.LevelDown(2);
            return table;
        }
    }
}
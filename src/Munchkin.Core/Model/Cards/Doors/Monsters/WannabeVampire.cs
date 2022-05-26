using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class WannabeVampire : MonsterCard
    {
        public WannabeVampire() :
            base(MunchkinDeluxeCards.Doors.WannabeVampire, "Wannabe Vampire", 12, 1, 3, 0, false)
        {
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            player.LevelDown(2);
            return table;
        }
    }
}
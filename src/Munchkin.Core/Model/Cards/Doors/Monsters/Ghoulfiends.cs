using Munchkin.Core.Contracts.Cards;
using System;
using System.Linq;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class Ghoulfiends : MonsterCard
    {
        public Ghoulfiends() :
            base(MunchkinDeluxeCards.Doors.Ghoulfiends, "Ghoulfiends", 8, 1, 2, 0, false)
        {
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            int minPlayerLevel = table.Players.Min(x => x.Level);
            player.LevelDown(player.Level - minPlayerLevel);
            return table;
        }
    }
}
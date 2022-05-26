using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class LameGoblin : MonsterCard
    {
        public LameGoblin() : 
            base(MunchkinDeluxeCards.Doors.LameGoblin, "Lame Goblin", 1, 1, 1, 1, false)
        {
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            player.LevelDown();
            return table;
        }
    }
}
using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class MisterBones : MonsterCard
    {
        public MisterBones() : 
            base(MunchkinDeluxeCards.Doors.MisterBones, "Mister Bones", 2, 1, 1, 0, true)
        {
            //TODO: lose level event if you escape
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
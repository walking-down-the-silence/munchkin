using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class Bullrog : MonsterCard
    {
        public Bullrog() :
            base(MunchkinDeluxeCards.Doors.Bullrog, "Bullrog", 18, 2, 5, 0, false)
        {
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            // TODO: double check: "will not pursue anyone with level 4 or below" 
            table.KillPlayer(player);

            return table;
        }
    }
}
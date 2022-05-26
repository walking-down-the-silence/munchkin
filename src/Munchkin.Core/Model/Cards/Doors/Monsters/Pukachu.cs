using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class Pukachu : MonsterCard
    {
        public Pukachu() : 
            base(MunchkinDeluxeCards.Doors.Pukachu, "Pukachu", 6, 1, 2, 0, false)
        {
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            throw new NotImplementedException();
        }
    }
}
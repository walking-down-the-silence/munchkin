using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class WightBrothers : MonsterCard
    {
        public WightBrothers() :
            base(MunchkinDeluxeCards.Doors.WightBrothers, "Wight Brothers", 16, 2, 4, 0, true)
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
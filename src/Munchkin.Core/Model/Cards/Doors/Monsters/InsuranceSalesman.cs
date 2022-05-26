using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class InsuranceSalesman : MonsterCard
    {
        public InsuranceSalesman() :
            base(MunchkinDeluxeCards.Doors.InsuranceSalesman, "Insurance Salesman", 14, 1, 4, 0, false)
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
using Munchkin.Core.Contracts.Cards;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class InsuranceSalesman : MonsterCard
    {
        public InsuranceSalesman() :
            base(MunchkinDeluxeCards.Doors.InsuranceSalesman, "Insurance Salesman", 14, 1, 4, 0, false)
        {
        }

        public override Task BadStuff(Table gameContext)
        {
            throw new NotImplementedException();
        }
    }
}
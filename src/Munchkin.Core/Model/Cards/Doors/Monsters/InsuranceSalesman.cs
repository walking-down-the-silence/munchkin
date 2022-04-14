using System;
using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class InsuranceSalesman : MonsterCard
    {
        public InsuranceSalesman() : base("Insurance Salesman", 14, 1, 4, 0, false)
        {
        }

        public override Task BadStuff(Table gameContext)
        {
            throw new NotImplementedException();
        }
    }
}
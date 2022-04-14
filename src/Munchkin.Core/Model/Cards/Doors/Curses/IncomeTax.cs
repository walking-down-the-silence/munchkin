using System;
using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class IncomeTax : CurseCard
    {
        public IncomeTax() : base("Income Tax")
        {
        }

        public override Task BadStuff(Table context)
        {
            throw new NotImplementedException();
        }
    }
}
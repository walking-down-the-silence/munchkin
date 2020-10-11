using System;
using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class IncomeTax : CurseCard
    {
        public IncomeTax() : base("Income Tax")
        {
        }

        public override Task Play(Table context)
        {
            throw new NotImplementedException();
        }
    }
}
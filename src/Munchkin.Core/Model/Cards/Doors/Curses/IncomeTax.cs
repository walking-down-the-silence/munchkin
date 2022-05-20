using Munchkin.Core.Contracts.Cards;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class IncomeTax : CurseCard
    {
        public IncomeTax() : 
            base(MunchkinDeluxeCards.Doors.IncomeTax, "Income Tax")
        {
        }

        public override Task BadStuff(Table context)
        {
            throw new NotImplementedException();
        }
    }
}
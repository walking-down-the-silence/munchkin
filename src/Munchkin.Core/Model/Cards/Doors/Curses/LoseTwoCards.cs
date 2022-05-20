using Munchkin.Core.Contracts.Cards;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class LoseTwoCards : CurseCard
    {
        public LoseTwoCards() : 
            base(MunchkinDeluxeCards.Doors.LoseTwoCards, "Lose Two Cards")
        {
        }

        public override Task BadStuff(Table context)
        {
            throw new NotImplementedException();
        }
    }
}
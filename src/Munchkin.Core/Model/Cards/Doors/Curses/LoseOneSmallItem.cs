using Munchkin.Core.Contracts.Cards;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class LoseOneSmallItem : CurseCard
    {
        public LoseOneSmallItem() : 
            base(MunchkinDeluxeCards.Doors.LoseOneSmallItem1, "Lose One Small Item")
        {
        }

        public override Task BadStuff(Table context)
        {
            throw new NotImplementedException();
        }
    }
}
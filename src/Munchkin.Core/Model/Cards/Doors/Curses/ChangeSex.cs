using Munchkin.Core.Contracts.Cards;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class ChangeSex : CurseCard
    {
        public ChangeSex() : 
            base(MunchkinDeluxeCards.Doors.ChangeSex, "Change Sex")
        {
        }

        public override Task BadStuff(Table context)
        {
            throw new NotImplementedException();
        }
    }
}

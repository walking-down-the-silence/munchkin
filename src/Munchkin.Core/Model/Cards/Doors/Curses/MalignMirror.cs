using Munchkin.Core.Contracts.Cards;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class MalignMirror : CurseCard
    {
        public MalignMirror() : 
            base(MunchkinDeluxeCards.Doors.MalignMirror, "Malign Mirror")
        {
        }

        public override Task BadStuff(Table context)
        {
            throw new NotImplementedException();
        }
    }
}
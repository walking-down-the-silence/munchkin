using Munchkin.Core.Contracts.Cards;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class LoseOneBigItem : CurseCard
    {
        public LoseOneBigItem() : 
            base(MunchkinDeluxeCards.Doors.LoseOneBigItem, "Lose One Big Item")
        {
        }

        public override Task BadStuff(Table context)
        {
            throw new NotImplementedException();
        }
    }
}
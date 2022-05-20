using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Attributes;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class ChickenOnYourHead : CurseCard
    {
        public ChickenOnYourHead() : 
            base(MunchkinDeluxeCards.Doors.ChickenOnYourHead, "Chiken On Your Head")
        {
            AddAttribute(new RunAwayBonusAttribute(-1));
        }

        public override Task BadStuff(Table context)
        {
            return Task.CompletedTask;
        }
    }
}
using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class TrulyObmoxiousCurse : CurseCard
    {
        public TrulyObmoxiousCurse() :
            base(MunchkinDeluxeCards.Doors.TrulyObmoxiousCurse, "Truly Obmoxious Curse")
        {
        }

        public override Task BadStuff(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
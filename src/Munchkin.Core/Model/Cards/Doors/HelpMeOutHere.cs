using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors
{
    public sealed class HelpMeOutHere : SpecialCard
    {
        public HelpMeOutHere() :
            base(MunchkinDeluxeCards.Doors.HelpMeOutHere, "Help Me Out Here")
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors
{
    public class Halfbreed : RaceCard
    {
        public Halfbreed() :
            base(MunchkinDeluxeCards.Doors.Halfbreed1, "Half-breed")
        {
        }

        public override Task Play(Table context) => Task.CompletedTask;
    }
}
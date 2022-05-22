using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Attributes;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors
{
    public class Halfbreed : RaceCard
    {
        public Halfbreed() :
            base(MunchkinDeluxeCards.Doors.Halfbreed1, "Half-breed")
        {
            AddAttribute(new MaximumEquippedRacesAttribute(2));
        }

        public override Task Play(Table context) => Task.CompletedTask;
    }
}
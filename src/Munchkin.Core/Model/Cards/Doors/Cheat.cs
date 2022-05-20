using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors
{
    public sealed class Cheat : SpecialCard
    {
        public Cheat() :
            base(MunchkinDeluxeCards.Doors.Cheat, "Cheat")
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors
{
    public sealed class Illusion : SpecialCard
    {
        public Illusion() :
            base(MunchkinDeluxeCards.Doors.Illusion, "Illusion")
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
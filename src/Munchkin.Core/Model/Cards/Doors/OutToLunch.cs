using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors
{
    public sealed class OutToLunch : SpecialCard
    {
        public OutToLunch() :
            base(MunchkinDeluxeCards.Doors.OutToLunch, "Out To Lunch")
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
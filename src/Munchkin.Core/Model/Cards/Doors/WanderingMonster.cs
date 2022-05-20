using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors
{
    public sealed class WanderingMonster : SpecialCard
    {
        public WanderingMonster() :
            base(MunchkinDeluxeCards.Doors.WanderingMonster1, "Wandering Monster")
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
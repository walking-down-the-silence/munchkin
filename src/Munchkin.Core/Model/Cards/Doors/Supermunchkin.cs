using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors
{
    public class SuperMunchkin : ClassCard
    {
        public SuperMunchkin() :
            base(MunchkinDeluxeCards.Doors.SuperMunchkin1, "Supermunchkin")
        {
        }

        public override Task Play(Table context) => Task.CompletedTask;
    }
}
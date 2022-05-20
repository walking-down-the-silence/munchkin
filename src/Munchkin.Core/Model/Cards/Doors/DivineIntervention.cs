using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Cards.Doors.Classes;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors
{
    public sealed class DivineIntervention : SpecialCard
    {
        public DivineIntervention() :
            base(MunchkinDeluxeCards.Doors.DivineIntervention, "Divine Intervention")
        {
        }

        public override Task Play(Table context)
        {
            context.Players
                .Where(player => player.Equipped.FirstOrDefault(x => x is ClericClass) != null)
                .ForEach(player => player.LevelUp());
            return Task.CompletedTask;
        }
    }
}
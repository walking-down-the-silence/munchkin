using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Attributes;
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

        public override Task Play(Table table)
        {
            table.Players
                .Where(player => player.Equipped.Any(x => x.HasAttribute<ClericAttribute>()))
                .ForEach(player => player.LevelUp());

            return Task.CompletedTask;
        }
    }
}
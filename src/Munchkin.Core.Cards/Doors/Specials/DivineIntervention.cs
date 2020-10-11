using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class DivineIntervention : SpecialCard
    {
        public DivineIntervention() : base("Divine Intervention")
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
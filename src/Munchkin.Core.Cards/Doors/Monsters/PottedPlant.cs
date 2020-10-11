using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class PottedPlant : MonsterCard
    {
        public PottedPlant() : base("PottedPlant", 1, 1, 1, 0, false)
        {
        }

        public override Task BadStuff(Table gameContext)
        {
            return Task.CompletedTask;
        }
    }
}
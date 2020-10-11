using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class DroolingSlime : MonsterCard
    {
        public DroolingSlime() : base("Drooling Slime", 1, 1, 1, 0, false)
        {
        }

        public override Task BadStuff(Table gameContext)
        {
            throw new System.NotImplementedException();
        }
    }
}
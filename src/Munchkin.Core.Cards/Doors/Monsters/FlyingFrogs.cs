using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class FlyingFrogs : MonsterCard
    {
        public FlyingFrogs() : base("Flying Frogs", 2, 1, 1, -1, false)
        {
        }

        public override Task BadStuff(Table gameContext)
        {
            throw new System.NotImplementedException();
        }
    }
}
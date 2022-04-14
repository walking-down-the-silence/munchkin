using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class FlyingFrogs : MonsterCard
    {
        public FlyingFrogs() : base("Flying Frogs", 2, 1, 1, -1, false)
        {
        }

        public override Task BadStuff(Table state)
        {
            state.Players.Current.LevelDown();
            state.Players.Current.LevelDown();

            return Task.CompletedTask;
        }
    }
}
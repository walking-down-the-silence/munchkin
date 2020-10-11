using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class Gazebo : MonsterCard
    {
        public Gazebo() : base("Gazebo", 8, 1, 2, 0, false)
        {
        }

        public override Task BadStuff(Table gameContext)
        {
            gameContext.Players.Current.LevelDown();
            gameContext.Players.Current.LevelDown();
            gameContext.Players.Current.LevelDown();
            return Task.CompletedTask;
        }
    }
}
using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class Gazebo : MonsterCard
    {
        public Gazebo() :
            base(MunchkinDeluxeCards.Doors.Gazebo, "Gazebo", 8, 1, 2, 0, false)
        {
            //TODO: no one can help you
        }

        public override Task BadStuff(Table state)
        {
            state.Players.Current.LevelDown();
            state.Players.Current.LevelDown();
            state.Players.Current.LevelDown();

            return Task.CompletedTask;
        }
    }
}
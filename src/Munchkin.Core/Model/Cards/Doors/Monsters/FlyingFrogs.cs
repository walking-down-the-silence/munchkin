using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class FlyingFrogs : MonsterCard
    {
        public FlyingFrogs() :
            base(MunchkinDeluxeCards.Doors.FlyingFrogs, "Flying Frogs", 2, 1, 1, -1, false)
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
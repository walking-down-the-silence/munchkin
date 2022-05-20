using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class LameGoblin : MonsterCard
    {
        public LameGoblin() : 
            base(MunchkinDeluxeCards.Doors.LameGoblin, "Lame Goblin", 1, 1, 1, 1, false)
        {
        }

        public override Task BadStuff(Table state)
        {
            state.Players.Current.LevelDown();
            return Task.CompletedTask;
        }
    }
}
using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class LameGoblin : MonsterCard
    {
        public LameGoblin() : base("Lame Goblin", 1, 1, 1, 1, false)
        {
        }

        public override Task BadStuff(Table state)
        {
            state.Players.Current.LevelDown();
            return Task.CompletedTask;
        }
    }
}
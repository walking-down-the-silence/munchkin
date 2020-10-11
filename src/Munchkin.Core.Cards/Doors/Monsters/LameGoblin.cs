using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class LameGoblin : MonsterCard
    {
        public LameGoblin() : base("Lame Goblin", 1, 1, 1, 1, false)
        {
        }

        public override Task BadStuff(Table gameContext)
        {
            gameContext.Players.Current.LevelDown();
            return Task.CompletedTask;
        }
    }
}
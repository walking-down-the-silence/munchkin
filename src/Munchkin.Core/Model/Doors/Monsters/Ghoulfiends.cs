using System.Linq;
using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class Ghoulfiends : MonsterCard
    {
        public Ghoulfiends() : base("Ghoulfiends", 8, 1, 2, 0, false)
        {
        }

        public override Task BadStuff(Table gameContext)
        {
            int minPlayerLevel = gameContext.Players.Min(x => x.Level);
            while (gameContext.Players.Current.Level > minPlayerLevel)
            {
                gameContext.Players.Current.LevelDown();
            }

            return Task.CompletedTask;
        }
    }
}
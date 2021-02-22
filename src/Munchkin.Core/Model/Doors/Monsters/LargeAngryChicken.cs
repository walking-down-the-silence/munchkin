using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class LargeAngryChicken : MonsterCard
    {
        public LargeAngryChicken() : base("Large Angry Chicken", 2, 1, 1, 0, false)
        {
        }

        public override Task BadStuff(Table gameContext)
        {
            gameContext.Players.Current.LevelDown();
            return Task.CompletedTask;
        }
    }
}
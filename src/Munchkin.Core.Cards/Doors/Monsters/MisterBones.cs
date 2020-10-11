using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class MisterBones : MonsterCard
    {
        public MisterBones() : base("Mister Bones", 2, 1, 1, 0, true)
        {
        }

        public override Task BadStuff(Table gameContext)
        {
            gameContext.Players.Current.LevelDown();
            gameContext.Players.Current.LevelDown();
            return Task.CompletedTask;
        }
    }
}
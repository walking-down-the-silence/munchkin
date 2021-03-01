using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class MisterBones : MonsterCard
    {
        public MisterBones() : base("Mister Bones", 2, 1, 1, 0, true)
        {
            //TODO: lose level event if you escape
        }

        public override Task BadStuff(Table state)
        {
            state.Players.Current.LevelDown();
            state.Players.Current.LevelDown();

            return Task.CompletedTask;
        }
    }
}
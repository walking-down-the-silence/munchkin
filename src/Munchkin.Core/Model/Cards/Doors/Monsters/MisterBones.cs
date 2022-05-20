using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class MisterBones : MonsterCard
    {
        public MisterBones() : 
            base(MunchkinDeluxeCards.Doors.MisterBones, "Mister Bones", 2, 1, 1, 0, true)
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
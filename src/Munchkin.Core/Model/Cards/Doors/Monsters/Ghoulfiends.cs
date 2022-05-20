using Munchkin.Core.Contracts.Cards;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class Ghoulfiends : MonsterCard
    {
        public Ghoulfiends() :
            base(MunchkinDeluxeCards.Doors.Ghoulfiends, "Ghoulfiends", 8, 1, 2, 0, false)
        {
            //TODO: fight with your level only
        }

        public override Task BadStuff(Table state)
        {
            int minPlayerLevel = state.Players.Min(x => x.Level);
            while (state.Players.Current.Level > minPlayerLevel)
            {
                state.Players.Current.LevelDown();
            }

            return Task.CompletedTask;
        }
    }
}
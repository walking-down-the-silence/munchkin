using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class Bullrog : MonsterCard
    {
        public Bullrog() :
            base(MunchkinDeluxeCards.Doors.Bullrog, "Bullrog", 18, 2, 5, 0, false)
        {
        }

        public override async Task BadStuff(Table state)
        {
            // TODO: double check: "will not pursue anyone with level 4 or below" 
            state.KillPlayer(state.Players.Current);
        }
    }
}
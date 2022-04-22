using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Services;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class Bullrog : MonsterCard
    {
        public Bullrog() : base("Bullrog", 18, 2, 5, 0, false)
        {
        }

        public override async Task BadStuff(Table state)
        {
            // TODO: double check: "will not pursue anyone with level 4 or below" 
            PlayerAvatar.Kill(state, state.Players.Current);
        }
    }
}
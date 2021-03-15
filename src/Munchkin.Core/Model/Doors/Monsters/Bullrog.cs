using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class Bullrog : MonsterCard
    {
        public Bullrog() : base("Bullrog", 18, 2, 5, 0, false)
        {
        }

        public override async Task BadStuff(Table state)
        {
            // TODO: double check: "will not pursue anyone with level 4 or below" 
            await state.Players.Current.Kill(state);
        }
    }
}
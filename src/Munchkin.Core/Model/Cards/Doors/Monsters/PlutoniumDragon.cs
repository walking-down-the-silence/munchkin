using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class PlutoniumDragon : MonsterCard
    {
        public PlutoniumDragon() :
            base(MunchkinDeluxeCards.Doors.PlutoniumDragon, "Plutonium Dragon", 20, 2, 5, 0, false)
        {
        }

        public override Task BadStuff(Table state)
        {
            if (state.Players.Current.Level > 5)
            {
                state.KillPlayer(state.Players.Current);
            }

            return Task.CompletedTask;
        }
    }
}
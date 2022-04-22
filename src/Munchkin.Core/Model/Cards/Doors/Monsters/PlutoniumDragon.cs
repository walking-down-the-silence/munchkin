using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Services;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class PlutoniumDragon : MonsterCard
    {
        public PlutoniumDragon() : base("Plutonium Dragon", 20, 2, 5, 0, false)
        {
        }

        public override Task BadStuff(Table state)
        {
            if (state.Players.Current.Level > 5)
            {
                PlayerAvatar.Kill(state, state.Players.Current);
            }

            return Task.CompletedTask;
        }
    }
}
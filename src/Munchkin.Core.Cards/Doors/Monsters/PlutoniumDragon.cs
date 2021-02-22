using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
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
                state.Players.Current.Kill(state);
            }

            return Task.CompletedTask;
        }
    }
}
using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class FloatingNose : MonsterCard
    {
        public FloatingNose() : base("Floating Nose", 10, 1, 3, 0, false)
        {
            //TODO: double check rules about fight it
        }

        public override Task BadStuff(Table state)
        {
            state.Players.Current.LevelDown();
            state.Players.Current.LevelDown();
            state.Players.Current.LevelDown();

            return Task.CompletedTask;
        }
    }
}
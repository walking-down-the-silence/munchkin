using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class DuckOfDoom : CurseCard
    {
        public DuckOfDoom() : base("Duck Of Doom")
        {
        }

        public override Task Play(Table context)
        {
            context.Players.Current.LevelDown();
            context.Players.Current.LevelDown();
            return Task.CompletedTask;
        }
    }
}
using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class LoseALevel : CurseCard
    {
        public LoseALevel() : base("Loose A Level")
        {
        }

        public override Task BadStuff(Table context)
        {
            context.Players.Current.LevelDown();
            return Task.CompletedTask;
        }
    }
}
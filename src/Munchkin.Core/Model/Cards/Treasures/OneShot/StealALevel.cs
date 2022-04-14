using System.Linq;
using System.Threading.Tasks;
using Munchkin.Core.Model;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class StealALevel : LevelUpTreasure
    {
        public StealALevel() : base("Steal A Level")
        {
        }

        public override Task Play(Table gameContext)
        {
            // select player to steal level from
            gameContext.Players.First().LevelDown();
            
            // level up the owner player
            return base.Play(gameContext);
        }
    }
}
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors
{
    public class Halfbreed : RaceCard
    {
        public Halfbreed() : base("Half-breed")
        {
        }

        public override Task Play(Table context) => Task.CompletedTask;
    }
}
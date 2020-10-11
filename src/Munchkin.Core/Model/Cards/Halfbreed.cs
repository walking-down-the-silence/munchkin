using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards
{
    public class Halfbreed : DoorsCard
    {
        public Halfbreed() : base("Half-breed")
        {
        }

        public override Task Play(Table context) => Task.CompletedTask;
    }
}
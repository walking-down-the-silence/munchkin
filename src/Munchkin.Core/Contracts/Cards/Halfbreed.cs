using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Cards
{
    public class Halfbreed : DoorsCard
    {
        public Halfbreed() : base("Half-breed")
        {
        }

        public override Task Play(Table context) => Task.CompletedTask;
    }
}
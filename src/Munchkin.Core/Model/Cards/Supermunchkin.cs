using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards
{
    public class SuperMunchkin : DoorsCard
    {
        public SuperMunchkin() : base("Supermunchkin")
        {
        }

        public override Task Play(Table context) => Task.CompletedTask;
    }
}
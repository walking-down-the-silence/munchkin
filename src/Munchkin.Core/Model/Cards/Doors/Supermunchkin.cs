using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Cards
{
    public class SuperMunchkin : ClassCard
    {
        public SuperMunchkin() : base("Supermunchkin")
        {
        }

        public override Task Play(Table context) => Task.CompletedTask;
    }
}
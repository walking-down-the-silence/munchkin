using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors
{
    public class SuperMunchkin : ClassCard
    {
        public SuperMunchkin() : base("Supermunchkin")
        {
        }

        public override Task Play(Table context) => Task.CompletedTask;
    }
}
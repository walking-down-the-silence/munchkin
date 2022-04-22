using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

namespace Munchkin.Core.Model.Cards.Doors
{
    public sealed class Cheat : SpecialCard
    {
        public Cheat() : base("Cheat")
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
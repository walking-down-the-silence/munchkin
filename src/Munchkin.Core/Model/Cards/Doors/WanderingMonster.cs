using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class WanderingMonster : SpecialCard
    {
        public WanderingMonster() : base("Wandering Monster")
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
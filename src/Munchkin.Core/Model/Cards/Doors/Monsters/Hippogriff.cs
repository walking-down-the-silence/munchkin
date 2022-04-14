using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class Hippogriff : MonsterCard
    {
        public Hippogriff() : base("Hippogriff", 16, 2, 4, 0, false)
        {
            //TODO: will not persue anyone with level 3 or below
        }

        public override Task BadStuff(Table state)
        {
            throw new System.NotImplementedException();
        }
    }
}
using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class Hippogriff : MonsterCard
    {
        public Hippogriff() : base("Hippogriff", 16, 2, 4, 0, false)
        {
        }

        public override Task BadStuff(Table gameContext)
        {
            throw new System.NotImplementedException();
        }
    }
}
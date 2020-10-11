using System;
using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class Pukachu : MonsterCard
    {
        public Pukachu() : base("Pukachu", 6, 1, 2, 0, false)
        {
        }

        public override Task BadStuff(Table gameContext)
        {
            throw new NotImplementedException();
        }
    }
}
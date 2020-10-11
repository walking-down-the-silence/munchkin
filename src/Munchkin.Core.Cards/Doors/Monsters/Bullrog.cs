using System;
using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class Bullrog : MonsterCard
    {
        public Bullrog() : base("Bullrog", 18, 2, 5, 0, false)
        {
        }

        public override Task BadStuff(Table gameContext)
        {
            throw new NotImplementedException();
        }
    }
}
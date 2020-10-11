using System;
using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class SnailsOnSpeed : MonsterCard
    {
        public SnailsOnSpeed() : base("Snails On Speed", 4, 1, 2, -2, false)
        {
        }

        public override Task BadStuff(Table gameContext)
        {
            throw new NotImplementedException();
        }
    }
}
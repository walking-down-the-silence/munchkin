using System;
using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class StonedGolem : MonsterCard
    {
        public StonedGolem() : base("Stoned Golem", 14, 1, 4, 0, false)
        {
        }

        public override Task BadStuff(Table gameContext)
        {
            throw new NotImplementedException();
        }
    }
}
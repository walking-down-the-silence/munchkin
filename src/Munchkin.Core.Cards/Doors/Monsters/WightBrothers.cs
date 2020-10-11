using System;
using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class WightBrothers : MonsterCard
    {
        public WightBrothers() : base("Wight Brothers", 16, 2, 4, 0, true)
        {
        }

        public override Task BadStuff(Table gameContext)
        {
            throw new NotImplementedException();
        }
    }
}
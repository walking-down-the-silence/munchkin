using System;
using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class Squidzilla : MonsterCard
    {
        public Squidzilla() : base("Squidzilla", 18, 2, 4, 0, false)
        {
        }

        public override Task BadStuff(Table gameContext)
        {
            throw new NotImplementedException();
        }
    }
}
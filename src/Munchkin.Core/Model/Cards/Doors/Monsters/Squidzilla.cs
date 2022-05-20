using Munchkin.Core.Contracts.Cards;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class Squidzilla : MonsterCard
    {
        public Squidzilla() :
            base(MunchkinDeluxeCards.Doors.Squidzilla, "Squidzilla", 18, 2, 4, 0, false)
        {
        }

        public override Task BadStuff(Table gameContext)
        {
            throw new NotImplementedException();
        }
    }
}
using Munchkin.Core.Contracts.Cards;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class KingTut : MonsterCard
    {
        public KingTut() :
            base(MunchkinDeluxeCards.Doors.KingTut, "King Tut", 16, 2, 4, 0, true)
        {
        }

        public override Task BadStuff(Table gameContext)
        {
            throw new NotImplementedException();
        }
    }
}
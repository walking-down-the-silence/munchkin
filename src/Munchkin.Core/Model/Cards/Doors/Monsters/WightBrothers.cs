using Munchkin.Core.Contracts.Cards;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class WightBrothers : MonsterCard
    {
        public WightBrothers() :
            base(MunchkinDeluxeCards.Doors.WightBrothers, "Wight Brothers", 16, 2, 4, 0, true)
        {
        }

        public override Task BadStuff(Table gameContext)
        {
            throw new NotImplementedException();
        }
    }
}
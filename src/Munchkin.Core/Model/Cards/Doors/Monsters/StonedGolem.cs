using Munchkin.Core.Contracts.Cards;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class StonedGolem : MonsterCard
    {
        public StonedGolem() :
            base(MunchkinDeluxeCards.Doors.StonedGolem, "Stoned Golem", 14, 1, 4, 0, false)
        {
        }

        public override Task BadStuff(Table gameContext)
        {
            throw new NotImplementedException();
        }
    }
}
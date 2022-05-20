using Munchkin.Core.Contracts.Cards;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class Pukachu : MonsterCard
    {
        public Pukachu() : 
            base(MunchkinDeluxeCards.Doors.Pukachu, "Pukachu", 6, 1, 2, 0, false)
        {
        }

        public override Task BadStuff(Table gameContext)
        {
            throw new NotImplementedException();
        }
    }
}
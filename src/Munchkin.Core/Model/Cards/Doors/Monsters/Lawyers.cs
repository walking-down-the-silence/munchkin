using Munchkin.Core.Contracts.Cards;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class Lawyers : MonsterCard
    {
        public Lawyers() : 
            base(MunchkinDeluxeCards.Doors.Lawyers, "Lawyers", 6, 1, 2, 0, false)
        {
        }

        public override Task BadStuff(Table gameContext)
        {
            throw new NotImplementedException();
        }
    }
}
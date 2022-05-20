using Munchkin.Core.Contracts.Cards;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Races
{
    public class HalflingRace : RaceCard
    {
        public HalflingRace() :
            base(MunchkinDeluxeCards.Doors.HalflingRace1, "Halfling")
        {
        }

        public override Task Play(Table context)
        {
            throw new NotImplementedException();
        }
    }
}
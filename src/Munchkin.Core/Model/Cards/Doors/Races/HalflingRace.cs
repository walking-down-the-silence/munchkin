using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Races
{
    public class HalflingRace : RaceCard
    {
        public HalflingRace() : base("Halfling")
        {
        }

        public override Task Play(Table context)
        {
            throw new NotImplementedException();
        }
    }
}
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using System;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
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
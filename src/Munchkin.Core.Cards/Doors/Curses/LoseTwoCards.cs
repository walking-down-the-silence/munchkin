using System;
using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class LoseTwoCards : CurseCard
    {
        public LoseTwoCards() : base("Lose Two Cards")
        {
        }

        public override Task BadStuff(Table context)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class LoseOneBigItem : CurseCard
    {
        public LoseOneBigItem() : base("Lose One Big Item")
        {
        }

        public override Task BadStuff(Table context)
        {
            throw new NotImplementedException();
        }
    }
}
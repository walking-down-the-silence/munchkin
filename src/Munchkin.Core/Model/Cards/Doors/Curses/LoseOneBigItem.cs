using System;
using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

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
using System;
using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class ChangeSex : CurseCard
    {
        public ChangeSex() : base("Change Sex")
        {
        }

        public override Task BadStuff(Table context)
        {
            throw new NotImplementedException();
        }
    }
}

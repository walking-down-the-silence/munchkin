using System;
using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class ChangeSex : CurseCard
    {
        public ChangeSex() : base("Change Sex")
        {
        }

        public override Task Play(Table context)
        {
            throw new NotImplementedException();
        }
    }
}

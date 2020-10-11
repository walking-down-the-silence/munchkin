using System;
using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class LoseOneSmallItem : CurseCard
    {
        public LoseOneSmallItem() : base("Lose One Small Item")
        {
        }

        public override Task Play(Table context)
        {
            throw new NotImplementedException();
        }
    }
}
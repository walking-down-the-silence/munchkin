using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class TrulyObmoxiousCurse : CurseCard
    {
        public TrulyObmoxiousCurse() : base("Truly Obmoxious Curse")
        {
        }

        public override Task BadStuff(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
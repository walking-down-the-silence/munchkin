using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class TrulyObmoxiousCurse : CurseCard
    {
        public TrulyObmoxiousCurse() : base("Truly Obmoxious Curse")
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
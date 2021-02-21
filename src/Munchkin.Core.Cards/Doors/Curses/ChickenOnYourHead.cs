using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Properties;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class ChickenOnYourHead : CurseCard
    {
        public ChickenOnYourHead() : base("Chiken On Your Head")
        {
            AddProperty(new RunAwayBonusAttribute(-1));
        }

        public override Task BadStuff(Table context)
        {
            return Task.CompletedTask;
        }
    }
}
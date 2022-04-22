using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Curses
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
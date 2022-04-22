using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

namespace Munchkin.Core.Model.Cards.Doors
{
    public sealed class HelpMeOutHere : SpecialCard
    {
        public HelpMeOutHere() : base("Help Me Out Here")
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
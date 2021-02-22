using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Engine.Original.Actions;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class ThiefClass : ClassCard
    {
        public ThiefClass() : base("Thief")
        {
            AddAction(new ActionDefinition<Table>("", () => new ThiefStealCardAction()));
            AddAction(new ActionDefinition<Table>("", () => new ThiefStabFor2Action()));
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
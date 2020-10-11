using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Engine.Original.Actions;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class ClericClass : ClassCard
    {
        public ClericClass() : base("Cleric")
        {
            AddAction(new ActionDefinition<Table>("", () => new ClericStrengthBonus3Action()));
            AddAction(new ActionDefinition<Table>("", () => new ClericReviveCardAction()));
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
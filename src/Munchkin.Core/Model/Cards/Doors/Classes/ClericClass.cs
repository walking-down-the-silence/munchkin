using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Actions;
using Munchkin.Core.Model.Attributes;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Classes
{
    public sealed class ClericClass : ClassCard
    {
        public ClericClass() :
            base(MunchkinDeluxeCards.Doors.ClericClass1, "Cleric")
        {
            AddAttribute(new ClericAttribute());
        }

        public IAction<Table> ReviveTheCard { get; private set; }

        public IAction<Table> AddStrengtAgainstUndead { get; private set; }

        public override Task Play(Table context)
        {
            // TODO: Owner here is null because it is not set yet
            ReviveTheCard = new ClericReviveCardAction(Owner);
            AddStrengtAgainstUndead = new ClericStrengthBonus3Action();

            return Task.CompletedTask;
        }
    }
}
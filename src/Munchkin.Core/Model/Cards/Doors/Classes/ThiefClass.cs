using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Actions;
using Munchkin.Core.Model.Attributes;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Classes
{
    public sealed class ThiefClass : ClassCard
    {
        public ThiefClass() :
            base(MunchkinDeluxeCards.Doors.ThiefClass1, "Thief")
        {
            AddAttribute(new ThiefAttribute());
        }

        public IAction<Table> StealTheCard { get; private set; }

        public IAction<Table> Backstab { get; private set; }

        public override Task Play(Table context)
        {
            // TODO: Owner here is null because it is not set yet
            StealTheCard = new ThiefStealCardAction();
            Backstab = new ThiefStabFor2Action();

            return Task.CompletedTask;
        }
    }
}
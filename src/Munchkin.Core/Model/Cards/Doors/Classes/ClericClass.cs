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

        public override Task Play(Table table)
        {
            Ressurect = new ClericRessurectionAction(Owner);
            Turning = new ClericTurningAction(Owner);

            return base.Play(table);
        }

        public ClericRessurectionAction Ressurect { get; private set; }

        public ClericTurningAction Turning { get; private set; }
    }
}
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Actions;
using Munchkin.Core.Model.Attributes;

namespace Munchkin.Core.Model.Cards.Doors.Classes
{
    public sealed class ClericClass : ClassCard
    {
        public ClericClass() :
            base(MunchkinDeluxeCards.Doors.ClericClass1, "Cleric")
        {
            AddAttribute(new ClericAttribute());

            Ressurect = new ClericRessurectionAction(Owner);
            Turning = new ClericTurningAction(Owner);
        }

        public ClericRessurectionAction Ressurect { get; }

        public ClericTurningAction Turning { get; }
    }
}
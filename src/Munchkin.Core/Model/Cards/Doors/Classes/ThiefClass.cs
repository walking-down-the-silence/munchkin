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

        public override Task Play(Table table)
        {
            Theft = new ThiefTheftAction(Owner);
            Backstabbing = new ThiefBackstabbingAction(Owner);

            return base.Play(table);
        }

        public ThiefTheftAction Theft { get; private set; }

        public ThiefBackstabbingAction Backstabbing { get; private set; }
    }
}
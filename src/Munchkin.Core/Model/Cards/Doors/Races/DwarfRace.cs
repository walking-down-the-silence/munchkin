using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Attributes;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Races
{
    public sealed class DwarfRace : RaceCard
    {
        public DwarfRace() :
            base(MunchkinDeluxeCards.Doors.DwarfRace1, "Dwarf")
        {
            AddAttribute(new DwarfAttribute());
            AddAttribute(new MaximumBigItemsCarriedAttribute(int.MaxValue));
            AddAttribute(new MaximumCardsInHandAttribute(6));
        }

        public override Task Play(Table context)
        {
            throw new NotImplementedException();
        }
    }
}

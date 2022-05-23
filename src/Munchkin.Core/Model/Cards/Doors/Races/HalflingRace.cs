using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Attributes;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Races
{
    public class HalflingRace : RaceCard
    {
        public HalflingRace() :
            base(MunchkinDeluxeCards.Doors.HalflingRace1, "Halfling")
        {
            AddAttribute(new HalflingAttribute());
        }

        public Table SellDoublePrice(Table table, ItemCard card)
        {
            throw new NotImplementedException();
        }

        public Table RerollTheDice(Table table, Card dicardCard)
        {
            throw new NotImplementedException();
        }
    }
}
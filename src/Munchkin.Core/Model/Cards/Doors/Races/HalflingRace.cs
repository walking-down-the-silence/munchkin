using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Attributes;
using Munchkin.Core.Model.Cards.Actions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Races
{
    public class HalflingRace : RaceCard
    {
        public HalflingRace() :
            base(MunchkinDeluxeCards.Doors.HalflingRace1, "Halfling")
        {
            AddAttribute(new HalflingAttribute());
        }

        public override Task Play(Table table)
        {
            SellDoublePrice = new HalflingSellDoublePriceAction(Owner);
            RerollTheDice = new HalflingRerollTheDiceAction(Owner);

            return base.Play(table);
        }

        public HalflingSellDoublePriceAction SellDoublePrice { get; private set; }

        public HalflingRerollTheDiceAction RerollTheDice { get; private set; }
    }
}
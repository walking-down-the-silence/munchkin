using Munchkin.Core.Extensions;
using Munchkin.Core.Model;

namespace Munchkin.Core.Contracts.Cards
{
    public abstract class TreasureCard : Card
    {
        protected TreasureCard(string title) : base(title)
        {
        }

        public override void Discard(Table state)
        {
            // put card to discard deck
            state.DiscardedTreasureCards.Put(this);

            // clean up the card
            Owner = null;
            BoundTo = null;

            // discard all bounded cards
            BoundCards.ForEach(card => card.Discard(state));
        }
    }
}
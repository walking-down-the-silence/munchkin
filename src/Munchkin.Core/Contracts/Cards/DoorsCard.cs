using Munchkin.Core.Extensions;
using Munchkin.Core.Model;

namespace Munchkin.Core.Contracts.Cards
{
    public abstract class DoorsCard : Card
    {
        protected DoorsCard(string code, string title) : 
            base(code, title)
        {
        }

        public override void Discard(Table state)
        {
            // put card to discard deck
            state.DiscardedDoorsCards.Put(this);

            // clean up the card
            Owner = null;
            BoundTo = null;

            // discard all bounded cards
            BoundCards.ForEach(card => card.Discard(state));
        }
    }
}
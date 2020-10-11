using Munchkin.Core.Extensions;

namespace Munchkin.Core.Model.Cards
{
    public abstract class DoorsCard : Card
    {
        protected DoorsCard(string title) : base(title)
        {
        }

        public override void Discard(Table context)
        {
            // remove card from player
            Owner?.Discard(this);

            // put card to discard deck
            context.DiscardedDoorsCards.Put(this);

            // clean up the card
            Owner = null;
            BoundTo = null;

            // discard all bounded cards
            BoundCards.ForEach(card => card.Discard(context));
        }
    }
}
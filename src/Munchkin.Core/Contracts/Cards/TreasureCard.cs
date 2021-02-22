using Munchkin.Core.Extensions;
using Munchkin.Core.Model;

namespace Munchkin.Core.Contracts.Cards
{
    public abstract class TreasureCard : Card
    {
        protected TreasureCard(string title) : base(title)
        {
        }

        public override void Discard(Table context)
        {
            // remove card from player
            Owner?.Discard(this);

            // put card to discard deck
            context.DiscardedTreasureCards.Put(this);

            // clean up the card
            Owner = null;
            BoundTo = null;

            // discard all bounded cards
            BoundCards.ForEach(card => card.Discard(context));
        }
    }
}
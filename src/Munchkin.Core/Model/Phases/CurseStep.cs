using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Primitives;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Phases
{
    /// <summary>
    /// Defines a process of resolving a curse sent onto a player.
    /// TODO: handle a case when the player does not have a wishing ring, but can play other card to obtain one.
    /// TODO: handle a case of highlighting the cards that can resolve a curse (not only Wishing Ring can resolve a curse).
    /// </summary>
    public class CurseStep : StepBase<Table>
    {
        public CurseStep(CurseCard curse) : base(StepNames.Curse)
        {
            CurseCard = curse ?? throw new ArgumentNullException(nameof(curse));

            // TODO: add this to the list of dynamic actions available to the player
            //var resolveCurseRequest = new PlayerChooseWishingRingOrContinueRequest(table.Players.Current, table);
        }

        public CardDeck<Card> TemporaryPile { get; }

        public CurseCard CurseCard { get; }

        protected override async Task<Table> OnResolve(Table table)
        {
            // NOTE: put all the cards from the temporary pile (cards played) into the discard pile
            table.DiscardedTreasureCards.PutRange(TemporaryPile.OfType<TreasureCard>());
            table.DiscardedDoorsCards.PutRange(TemporaryPile.OfType<DoorsCard>());

            return table;
        }
    }
}

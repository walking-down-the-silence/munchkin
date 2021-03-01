using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.States;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Attributes;
using Munchkin.Core.Model.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class CurseStage : State, IStage
    {
        private readonly List<Card> _playedCards;

        public CurseStage(CurseCard curse, List<Card> playedCards)
        {
            _playedCards = playedCards ?? throw new System.ArgumentNullException(nameof(playedCards));
            Curse = curse ?? throw new System.ArgumentNullException(nameof(curse));
        }

        public CurseCard Curse { get; }

        public bool IsTerminal => false;

        public IReadOnlyCollection<Card> PlayedCards => _playedCards.AsReadOnly();

        public async Task<IStage> Resolve(Table table)
        {
            // TODO: handle a case when the player does not have a wishing ring, but can play other card to obtain one
            var resolveCurseRequest = new PlayerChooseWishingRingOrContinueRequest(table.Players.Current, table);
            var resolveCurseResponse = await table.RequestSink.Send(resolveCurseRequest);
            var resolveCurseAction = await resolveCurseResponse.Task;

            if (resolveCurseAction == PlayWishingRingOrContinueActions.PlayWishingRing)
            {
                var curseCancellableCards = table.Players.Current.YourHand
                    .Concat(table.Players.Current.Backpack)
                    .Where(card => card.HasAttribute<CancelCurseAttribute>())
                    .ToArray();
                var selectCurseCancellableCardRequest = new PlayerSelectSingleCardRequest(table.Players.Current, table, curseCancellableCards);
                var selectCurseCancellableCardResponse = await table.RequestSink.Send(selectCurseCancellableCardRequest);
                var selectCurseCancellableCard = await selectCurseCancellableCardResponse.Task;

                if (selectCurseCancellableCard is null)
                {
                    await TakeBadStuff(table, table.Players.Current);
                }
            }
            else
            {
                await TakeBadStuff(table, table.Players.Current);
            }

            return new EmptyStage(_playedCards);
        }

        private async Task TakeBadStuff(Table table, Player player)
        {
            // TODO: pass a player to take the bad stuff (for a case with helping player)
            await Curse.BadStuff(table);
        }
    }
}

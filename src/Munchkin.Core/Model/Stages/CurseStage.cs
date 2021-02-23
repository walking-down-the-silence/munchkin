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
        private readonly Table _table;
        private readonly List<Card> _playedCards;

        public CurseStage(Table table, CurseCard curse, List<Card> playedCards)
        {
            _table = table ?? throw new System.ArgumentNullException(nameof(table));
            _playedCards = playedCards ?? throw new System.ArgumentNullException(nameof(playedCards));
            Curse = curse ?? throw new System.ArgumentNullException(nameof(curse));
        }

        public CurseCard Curse { get; }

        public bool IsTerminal => false;

        public IReadOnlyCollection<Card> PlayedCards => _playedCards.AsReadOnly();

        public async Task<IStage> Resolve()
        {
            // TODO: handle a case when the player does not have a wishing ring, but can play other card to obtain one
            var resolveCurseRequest = new PlayWishingRingOrContinueRequest(_table.Players.Current, _table);
            var resolveCurseResponse = await _table.RequestSink.Send(resolveCurseRequest);
            var resolveCurseAction = await resolveCurseResponse.Task;

            if (resolveCurseAction == PlayWishingRingOrContinueActions.PlayWishingRing)
            {
                var curseCancellableCards = _table.Players.Current.YourHand
                    .Concat(_table.Players.Current.Backpack)
                    .Where(card => card.HasAttribute<CancelCurseAttribute>())
                    .ToArray();
                var selectCurseCancellableCardRequest = new SelectCardsRequest(_table.Players.Current, _table, curseCancellableCards);
                var selectCurseCancellableCardResponse = await _table.RequestSink.Send(selectCurseCancellableCardRequest);
                var selectCurseCancellableCard = await selectCurseCancellableCardResponse.Task;

                if (selectCurseCancellableCard is null)
                {
                    await TakeBadStuff(_table.Players.Current);
                }
            }
            else
            {
                await TakeBadStuff(_table.Players.Current);
            }

            var furtherActionRequest = new LookForTroubleOrLootTheRoomRequest(_table.Players.Current, _table);
            var furtherActionResponse = await _table.RequestSink.Send(furtherActionRequest);
            var furtherAction = await furtherActionResponse.Task;
            bool lookForTrouble = furtherAction == EmptyRoomActions.LookForTrouble;
            return lookForTrouble ? await LookForTrouble() : await LootTheRoom();
        }

        public Task<IStage> LootTheRoom()
        {
            // TODO: check if deck is empty and reshuffle discard if it is
            DoorsCard doorsCard = _table.DoorsCardDeck.Take();
            _table.Players.Current.TakeInHand(doorsCard);
            IStage stage = new EndStage(_table, _playedCards);
            return Task.FromResult(stage);
        }

        public async Task<IStage> LookForTrouble()
        {
            var monsters = _table.Players.Current.YourHand.OfType<MonsterCard>().ToList();
            var request = new SelectMonsterFromHandRequest(_table.Players.Current, _table, monsters);
            var response = await _table.RequestSink.Send(request);
            var monsterCard = await response.Task;
            return new CombatStage(_table, monsterCard, _playedCards);
        }

        private async Task TakeBadStuff(Player player)
        {
            // TODO: pass a player to take the bad stuff (for a case with helping player)
            await Curse.BadStuff(_table);
        }
    }
}

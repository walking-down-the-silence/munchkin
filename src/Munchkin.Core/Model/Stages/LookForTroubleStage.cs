using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.States;
using Munchkin.Core.Model.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    /// <summary>
    /// Prompts a request to the player to select a monster from hand, if any.
    /// </summary>
    public class LookForTroubleStage : State, IStage
    {
        private readonly List<Card> _playedCards;

        public LookForTroubleStage(List<Card> playedCards)
        {
            _playedCards = playedCards ?? throw new System.ArgumentNullException(nameof(playedCards));
        }

        public bool IsTerminal => false;

        public IReadOnlyCollection<Card> PlayedCards => _playedCards.AsReadOnly();


        public async Task<IStage> Resolve(Table table)
        {
            var monsters = table.Players.Current.YourHand.OfType<MonsterCard>().ToList();
            var request = new PlayerSelectMonsterFromHandRequest(table.Players.Current, table, monsters);
            var response = await table.RequestSink.Send(request);
            var monsterCard = await response.Task;
            return new CombatStage(monsterCard, _playedCards);
        }
    }
}

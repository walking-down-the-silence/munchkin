using Munchkin.Core.Contracts;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Requests;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class EmptyStage : State, IStage
    {
        private readonly Table _table;

        public EmptyStage(Table table)
        {
            _table = table ?? throw new System.ArgumentNullException(nameof(table));
        }

        public bool IsTerminal => false;

        public async Task<IStage> Resolve()
        {
            var request = new LookForTroubleOrLootTheRoomRequest(_table.Players.Current, _table);
            var response = await _table.RequestSink.Send(request);
            var action = await response.Task;
            bool lookForTrouble = action == EmptyRoomActions.LookForTrouble;
            return lookForTrouble ? await LookForTrouble() : await LootTheRoom();
        }

        /// <summary>
        /// Takes a card from the doors deck and puts in hand.
        /// </summary>
        /// <returns>The end stage.</returns>
        public async Task<IStage> LootTheRoom()
        {
            // TODO: check if deck is empty and reshuffle discard if it is
            var doorsCard = _table.DoorsCardDeck.Take();
            _table.Players.Current.TakeInHand(doorsCard);
            return new EndStage(_table);
        }

        /// <summary>
        /// Prompts a request to the player to select a monster from hand, if any.
        /// </summary>
        /// <returns>The combat stage.</returns>
        public async Task<IStage> LookForTrouble()
        {
            var monsters = _table.Players.Current.YourHand.OfType<MonsterCard>().ToList();
            var request = new SelectMonsterFromHandRequest(_table.Players.Current, _table, monsters);
            var response = await _table.RequestSink.Send(request);
            var monsterCard = await response.Task;
            return new CombatStage(_table, monsterCard);
        }
    }
}

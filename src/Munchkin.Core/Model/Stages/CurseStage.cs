using Munchkin.Core.Contracts;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Requests;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class CurseStage : State, IStage
    {
        private readonly Table _table;

        public CurseStage(Table table, CurseCard curse)
        {
            _table = table ?? throw new System.ArgumentNullException(nameof(table));
            Curse = curse ?? throw new System.ArgumentNullException(nameof(curse));
            LastCardPlayed = curse;
        }

        public Dungeon Dungeon { get; }

        public CurseCard Curse { get; }

        public Card LastCardPlayed { get; }

        public bool IsTerminal => false;

        public async Task<IStage> Resolve()
        {
            // TODO: prompt the player to handle the curse by either playing a Wishing Ring card or by Taking The Bad Stuff


            var request = new LookForTroubleOrLootTheRoomRequest(_table.Players.Current, _table);
            var response = await _table.RequestSink.Send(request);
            var action = await response.Task;
            bool lookForTrouble = action == EmptyRoomActions.LookForTrouble;
            return lookForTrouble ? await LookForTrouble() : await LootTheRoom();
        }

        public async Task<IStage> LootTheRoom()
        {
            // TODO: check if deck is empty and reshuffle discard if it is
            DoorsCard doorsCard = _table.DoorsCardDeck.Take();
            _table.Players.Current.TakeInHand(doorsCard);
            return new EndStage(_table);
        }

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

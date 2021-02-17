using Munchkin.Core.Model.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.States
{
    public class EmptyStage : State, IStage
    {
        private readonly Table _table;

        public EmptyStage(Table table)
        {
            _table = table ?? throw new System.ArgumentNullException(nameof(table));
        }

        public bool IsTerminal => false;

        public Task<IStage> Resolve()
        {
            // TODO: prompt the player to either loot the room or look for trouble
            bool lookForTrouble = true;
            return lookForTrouble ? LookForTrouble() : LootTheRoom();
        }

        public async Task<IStage> LootTheRoom()
        {
            DoorsCard doorsCard = _table.DoorsCardDeck.Take();
            _table.Players.Current.TakeInHand(doorsCard);
            return new EndStage(_table);
        }

        public async Task<IStage> LookForTrouble()
        {
            // TODO: prompt a request to the player to select a monster from hand
            MonsterCard monsterCard = default;
            return new CombatStage(_table, monsterCard);
        }
    }
}

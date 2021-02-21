using Munchkin.Core.Contracts;
using Munchkin.Core.Model.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class RoomStage : State, IStage
    {
        private readonly Table _table;
        private readonly DoorsCard _doorsCard;

        public RoomStage(Table table, DoorsCard doorsCard)
        {
            _table = table ?? throw new System.ArgumentNullException(nameof(table));
            _doorsCard = doorsCard ?? throw new System.ArgumentNullException(nameof(doorsCard));
        }

        public bool IsTerminal => false;

        public Task<IStage> Resolve()
        {
            return _doorsCard switch
            {
                MonsterCard monsterCard => Fight(monsterCard),
                CurseCard curseCard => HandleCurse(curseCard),
                _ => TakeInHand(_doorsCard)
            };
        }

        private async Task<IStage> Fight(MonsterCard monsterCard)
        {
            return new CombatStage(_table, monsterCard);
        }

        private async Task<IStage> HandleCurse(CurseCard curseCard)
        {
            return new CurseStage(_table, curseCard);
        }

        private async Task<IStage> TakeInHand(DoorsCard doorsCard)
        {
            _table.Players.Current.TakeInHand(doorsCard);
            return new EmptyStage(_table);
        }
    }
}

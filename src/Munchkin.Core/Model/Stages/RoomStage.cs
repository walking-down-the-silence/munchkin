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

        private Task<IStage> Fight(MonsterCard monsterCard)
        {
            IStage stage = new CombatStage(_table, monsterCard);
            return Task.FromResult(stage);
        }

        private Task<IStage> HandleCurse(CurseCard curseCard)
        {
            IStage stage = new CurseStage(_table, curseCard);
            return Task.FromResult(stage);
        }

        private Task<IStage> TakeInHand(DoorsCard doorsCard)
        {
            _table.Players.Current.TakeInHand(doorsCard);
            IStage stage = new EmptyStage(_table);
            return Task.FromResult(stage);
        }
    }
}

using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class RoomStage : State, IStage
    {
        private readonly Table _table;
        private readonly DoorsCard _doorsCard;
        private readonly List<Card> _playedCards;

        public RoomStage(Table table, DoorsCard doorsCard)
        {
            _table = table ?? throw new System.ArgumentNullException(nameof(table));
            _doorsCard = doorsCard ?? throw new System.ArgumentNullException(nameof(doorsCard));
            _playedCards = new List<Card> { doorsCard };
        }

        public bool IsTerminal => false;

        public IReadOnlyCollection<Card> PlayedCards => _playedCards.AsReadOnly();

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
            IStage stage = new CombatStage(_table, monsterCard, _playedCards);
            return Task.FromResult(stage);
        }

        private Task<IStage> HandleCurse(CurseCard curseCard)
        {
            IStage stage = new CurseStage(_table, curseCard, _playedCards);
            return Task.FromResult(stage);
        }

        private Task<IStage> TakeInHand(DoorsCard doorsCard)
        {
            _table.Players.Current.TakeInHand(doorsCard);
            IStage stage = new EmptyStage(_table, _playedCards);
            return Task.FromResult(stage);
        }
    }
}

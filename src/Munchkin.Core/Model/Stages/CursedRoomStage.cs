using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class CursedRoomStage : IHierarchialStep<Table>
    {
        private readonly List<Card> _playedCards;

        public CursedRoomStage(CurseCard curse, List<Card> playedCards)
        {
            _playedCards = playedCards ?? throw new System.ArgumentNullException(nameof(playedCards));
            Curse = curse ?? throw new System.ArgumentNullException(nameof(curse));
        }

        public CurseCard Curse { get; }

        public async Task<Table> Resolve(Table table)
        {
            var curseStage = new CurseStep(Curse, _playedCards);
            table = await curseStage.Resolve(table);

            var emptyRoomStage = new EmptyRoomStep(_playedCards);
            return await emptyRoomStage.Resolve(table);
        }
    }
}

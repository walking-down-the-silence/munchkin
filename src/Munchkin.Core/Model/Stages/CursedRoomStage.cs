using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Stages;

namespace Munchkin.Core.Model.Stages
{
    public class CursedRoomStage : SequenceStep<Table>
    {
        public CursedRoomStage(CurseCard curse)
        {
            AddStep(new CurseStep(curse));
            AddStep(new EmptyRoomStep());
        }
    }
}

using Munchkin.Core.Model;
using Munchkin.Core.Model.Phases;
using Munchkin.Core.Primitives;

namespace Munchkin.Core.Tests.Primitives
{
    public class EmptyRoomStep : StepBase<Table>
    {
        public EmptyRoomStep() : base(StepNames.EmptyRoom)
        {
        }
    }
}
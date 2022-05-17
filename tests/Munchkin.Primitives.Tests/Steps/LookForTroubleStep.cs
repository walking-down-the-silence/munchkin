using Munchkin.Core.Model;
using Munchkin.Core.Model.Phases;
using Munchkin.Core.Primitives;

namespace Munchkin.Core.Tests.Primitives
{
    public class LookForTroubleStep : StepBase<Table>
    {
        public LookForTroubleStep() : base(StepNames.LookForTrouble)
        {
        }
    }
}
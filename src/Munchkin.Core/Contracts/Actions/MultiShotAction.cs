using Munchkin.Core.Model;

namespace Munchkin.Core.Contracts.Actions
{
    public abstract class MultiShotAction : DynamicAction, IMultiShotAction<Table>
    {
        protected MultiShotAction(int executionCount, string title, string description) : base(title, description)
        {
            ExecutionsCount = executionCount;
            ExecutionsLeft = executionCount;
        }

        public int ExecutionsCount { get; }

        public int ExecutionsLeft { get; }
    }
}

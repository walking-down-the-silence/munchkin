using MediatR;
using Munchkin.Core.Contracts.Actions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Actions
{
    public class PlayerNextStageAction : DynamicAction, IOneShotAction<Table>
    {
        private readonly TaskCompletionSource<Unit> _taskCompletionSource;
        private bool _wasExecuted = false;

        public PlayerNextStageAction(TaskCompletionSource<Unit> taskCompletionSource) : base("Next Stage", "")
        {
            _taskCompletionSource = taskCompletionSource ?? throw new System.ArgumentNullException(nameof(taskCompletionSource));
        }

        public override bool CanExecute(Table state) => !_wasExecuted;

        public override Task<Table> ExecuteAsync(Table table)
        {
            _taskCompletionSource.SetResult(Unit.Value);
            _wasExecuted = true;
            return Task.FromResult(table);
        }
    }
}
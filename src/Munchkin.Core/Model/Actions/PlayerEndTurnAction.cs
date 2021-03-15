using MediatR;
using Munchkin.Core.Contracts.Actions;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Actions
{
    public class PlayerEndTurnAction : DynamicAction
    {
        private readonly TaskCompletionSource<Unit> _taskCompletionSource;

        public PlayerEndTurnAction(TaskCompletionSource<Unit> taskCompletionSource) : base("End Turn", "")
        {
            _taskCompletionSource = taskCompletionSource ?? throw new ArgumentNullException(nameof(taskCompletionSource));
        }

        public override bool CanExecute(Table state)
        {
            return !_taskCompletionSource.Task.IsCompleted;
        }

        public override Task<Table> ExecuteAsync(Table state)
        {
            _taskCompletionSource.SetResult(Unit.Value);
            return Task.FromResult(state);
        }
    }
}
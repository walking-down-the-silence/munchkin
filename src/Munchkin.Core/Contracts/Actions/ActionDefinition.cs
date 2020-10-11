using System;

namespace Munchkin.Core.Contracts
{
    public class ActionDefinition<TState> : IActionDefinition<TState>
    {
        private readonly Func<IAction<TState>> _creator;

        public ActionDefinition(string title, Func<IAction<TState>> creator)
        {
            Title = title;
            _creator = creator;
        }

        public string Title { get; }

        public IAction<TState> Create() => _creator.Invoke();
    }
}

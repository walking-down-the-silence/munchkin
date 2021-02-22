using Munchkin.Core.Model;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Actions
{
    public abstract class DynamicAction : IAction<Table>
    {
        protected DynamicAction(string title, string description)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public string Title { get; }

        public string Description { get; }

        public abstract bool CanExecute(Table state);

        public abstract Task<Table> ExecuteAsync(Table state);
    }
}

namespace Munchkin.Core.Contracts.Actions
{
    public abstract class ActionBase : IAction
    {
        protected ActionBase(string type, string title)
        {
            Type = type;
            Title = title;
        }

        public string Type { get; }

        public string Title { get; }
    }
}

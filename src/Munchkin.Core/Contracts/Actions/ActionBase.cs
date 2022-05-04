namespace Munchkin.Core.Contracts.Actions
{
    public record ActionBase(string Type, string Title, string Description) :
        IAction;
}

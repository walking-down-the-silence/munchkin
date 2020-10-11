namespace Munchkin.Core.Contracts
{
    /// <summary>
    /// Defines an active trait that can be executed once or multiplle times per turn.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public interface IActiveAttribute<TState> : IAttribute
    {
        IAction<TState> ToAction();
    }
}

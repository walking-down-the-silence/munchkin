using Munchkin.Core.Contracts.Events;

namespace Munchkin.Core.Model.Phases.Events
{
    /// <summary>
    /// A marker interface that defines the start of some new state.
    /// </summary>
    public interface IEnteredStateEvent : IEvent
    {
    }
}
using System;

namespace Munchkin.Core.Model.Phases.Events
{
    public interface IRunningAwayEvent
    {
        DateTimeOffset CreatedDate { get; }
    }
}

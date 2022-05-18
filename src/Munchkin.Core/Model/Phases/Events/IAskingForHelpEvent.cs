using System;

namespace Munchkin.Core.Model.Phases.Events
{
    public interface IAskingForHelpEvent
    {
        DateTimeOffset CreatedDate { get; }
    }
}

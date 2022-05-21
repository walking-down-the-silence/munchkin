using System;

namespace Munchkin.Core.Contracts.Events
{
    public interface IEvent
    {
        DateTimeOffset CreatedDate { get; }
    }
}
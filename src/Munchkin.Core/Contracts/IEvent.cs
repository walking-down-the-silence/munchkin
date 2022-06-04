using System;

namespace Munchkin.Core.Contracts
{
    public interface IEvent
    {
        DateTimeOffset CreatedDate { get; }
    }
}
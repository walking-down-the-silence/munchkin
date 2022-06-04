using System;

namespace Munchkin.Core.Contracts
{
    public abstract record EventBase(DateTimeOffset CreatedDate) :
        IEvent;
}

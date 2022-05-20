using System;

namespace Munchkin.Core.Contracts.Events
{
    public abstract record EventBase(DateTimeOffset CreatedDate);
}

using System;

namespace Munchkin.Core.Contracts.Events
{
    public record PlayerCardSoldEvent(string PlayerNickname, string CardId, int GoldPieces) :
        EventBase(DateTimeOffset.UtcNow);
}

using System.Collections.Generic;

namespace Munchkin.Core.Stages
{
    public interface ITradePersistance
    {
        Trade GetTradeByIdAsync(string tradeId);

        IReadOnlyCollection<Trade> GetTradesAsync(string tableId);

        Trade SaveTradeAsync(Trade trade);
    }
}

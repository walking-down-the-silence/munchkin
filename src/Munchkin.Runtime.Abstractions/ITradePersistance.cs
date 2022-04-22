using Munchkin.Core.Model.Phases.Trades;
using System.Collections.Generic;

namespace Munchkin.Runtime.Abstractions
{
    public interface ITradePersistance
    {
        Trade GetTradeByIdAsync(string tradeId);

        IReadOnlyCollection<Trade> GetTradesAsync(string tableId);

        Trade SaveTradeAsync(Trade trade);
    }
}

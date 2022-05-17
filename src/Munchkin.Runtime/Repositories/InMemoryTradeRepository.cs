using Munchkin.Core.Model.Phases.Trades;
using Munchkin.Runtime.Abstractions;
using System;
using System.Collections.Generic;

namespace Munchkin.Runtime.Repositories
{
    public class InMemoryTradeRepository : ITradeRepository
    {
        public Trade GetTradeByIdAsync(string tradeId)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<Trade> GetTradesAsync(string tableId)
        {
            throw new NotImplementedException();
        }

        public Trade SaveTradeAsync(Trade trade)
        {
            throw new NotImplementedException();
        }
    }
}

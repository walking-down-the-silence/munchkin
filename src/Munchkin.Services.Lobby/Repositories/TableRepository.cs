using Munchkin.Runtime.Abstractions.Tables;
using Orleans;
using System;
using System.Threading.Tasks;

namespace Munchkin.Services.Lobby.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly IClusterClient _clusterClient;

        public TableRepository(
            IClusterClient clusterClient)
        {
            _clusterClient = clusterClient ?? throw new ArgumentNullException(nameof(clusterClient));
        }

        public Task<ITable> GetTableByIdAsync(string tableId)
        {
            var table = _clusterClient.GetGrain<ITable>(tableId);
            return Task.FromResult(table);
        }

        public Task<ITable> SaveTableAsync(ITable gameRoom) => throw new NotImplementedException();

        public Task<bool> DropTableAsync(string tableId) => throw new NotImplementedException();
    }
}

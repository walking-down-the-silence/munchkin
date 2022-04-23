using Munchkin.Core.Model.Expansions;
using Munchkin.Extensions.Threading;
using Munchkin.Runtime.Abstractions;
using Orleans;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Services.Lobby.Services
{
    public class TableService
    {
        private readonly IClusterClient _clusterClient;
        private readonly IPlayerRepository _playerRepository;

        public TableService(
            IClusterClient clusterClient,
            IPlayerRepository playerRepository)
        {
            _clusterClient = clusterClient ?? throw new ArgumentNullException(nameof(clusterClient));
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }

        public Task<ITable> CreateAsync() =>
            GenerateUniqueId()
                .Unit()
                .SelectMany(tableId => _clusterClient.GetGrain<ITable>(tableId).Unit());

        public Task<ITable> GetAsync(string tableId) =>
            _clusterClient.GetGrain<ITable>(tableId).Unit();

        public Task<ITable> SetupAsync(string tableId) =>
            _clusterClient
                .GetGrain<ITable>(tableId)
                .SetupAsync()
                .SelectMany(table => table.AsReference<ITable>().Unit());

        public Task<JoinTableResult> JoinTableAsync(string tableId, string nickname) =>
            _clusterClient.GetGrain<ITable>(tableId).Unit()
                .SelectMany(table => _playerRepository
                    .GetPlayerByNicknameAsync(nickname)
                    .SelectMany(player => table.JoinAsync(player)));

        public Task<JoinTableResult> LeaveTableAsync(string tableId, string nickname) =>
            _clusterClient.GetGrain<ITable>(tableId).Unit()
                .SelectMany(table => _playerRepository
                    .GetPlayerByNicknameAsync(nickname)
                    .SelectMany(player => table.LeaveAsync(player)));

        public Task<SelectExpansionResult> MarkExpansionSelectionAsync(string tableId, string expansionCode, bool selected) =>
            _clusterClient.GetGrain<ITable>(tableId).Unit()
                .SelectMany(table => selected
                    ? table.IncludeExpansionAsync(expansionCode)
                    : table.ExcludeExpansionAsync(expansionCode));

        private static string GenerateUniqueId() => $"table_{Guid.NewGuid()}";
    }
}

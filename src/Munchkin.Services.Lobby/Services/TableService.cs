using Microsoft.Extensions.DependencyInjection;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Expansions;
using Munchkin.Runtime.Abstractions.Tables;
using Munchkin.Runtime.Abstractions.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Services.Lobby.Services
{
    public class TableService
    {
        private readonly IPlayerRepository _userRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IServiceProvider _expansionProvider;

        public TableService(
            IPlayerRepository userRepository,
            ITableRepository tableRepository,
            IServiceProvider expansionProvider)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _tableRepository = tableRepository ?? throw new ArgumentNullException(nameof(tableRepository));
            _expansionProvider = expansionProvider ?? throw new ArgumentNullException(nameof(expansionProvider));
        }

        public async Task<ITable> CreateTableAsLeader(string nickname)
        {
            var availableExpansions = _expansionProvider
                .GetServices<IExpansion>()
                .Select(x => new ExpansionOption(x.Code, x.Title))
                .ToArray();

            var user = await _userRepository.GetPlayerByNicknameAsync(nickname);

            // TODO: generate a unique id for the table
            var tableId = "table_123";
            var table = await _tableRepository.GetTableByIdAsync(tableId);
            await table.WithExpansions(availableExpansions);

            var joinResponse = user is not null
                ? await table.JoinRoom(user)
                : JoinTableResult.InvalidUser;

            return joinResponse == JoinTableResult.JoinedRoom ? table : default;
        }

        public async Task<ITable> GetTable(string tableId)
        {
            return await _tableRepository.GetTableByIdAsync(tableId);
        }

        public async Task<IReadOnlyCollection<Player>> GetPlayersAsync(string tableId)
        {
            var table = await _tableRepository.GetTableByIdAsync(tableId);
            var players = await table.GetPlayers();
            return players;
        }

        public async Task<Player> GetPlayerByIdAsync(string tableId, string nickname)
        {
            var table = await _tableRepository.GetTableByIdAsync(tableId);
            var players = await table.GetPlayers();
            return players.SingleOrDefault(x => x.Nickname == nickname);
        }

        public async Task<JoinTableResult> JoinTable(string tableId, string nickname)
        {
            var user = await _userRepository.GetPlayerByNicknameAsync(nickname);
            var table = await _tableRepository.GetTableByIdAsync(tableId);
            var joinResponse = user is not null
                ? await table.JoinRoom(user)
                : JoinTableResult.InvalidUser;
            return joinResponse;
        }

        public async Task<JoinTableResult> LeaveTable(string tableId, string nickname)
        {
            var player = await _userRepository.GetPlayerByNicknameAsync(nickname);
            var table = await _tableRepository.GetTableByIdAsync(tableId);
            var joinResponse = player is not null
                ? await table.LeaveRoom(player)
                : JoinTableResult.InvalidUser;
            return joinResponse;
        }

        public async Task<IReadOnlyCollection<ExpansionSelection>> GetExpansionSelections(string tableId)
        {
            var table = await _tableRepository.GetTableByIdAsync(tableId);
            return await table.GetExpansionSelections();
        }

        public async Task<SelectExpansionResult> MarkExpansionSelection(string tableId, string expansionCode, bool selected)
        {
            var table = await _tableRepository.GetTableByIdAsync(tableId);
            var result = selected
                ? await table.SelectExpansion(expansionCode)
                : await table.UnselectExpansion(expansionCode);
            return result;
        }
    }
}

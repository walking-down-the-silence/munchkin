using Munchkin.Core.Model;
using Munchkin.Core.Model.Expansions;
using Munchkin.Runtime.Abstractions.Tables;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Services
{
    public class Table : Grain, ITable
    {
        private readonly Dictionary<string, Player> _players = new();
        private readonly List<ExpansionOption> _expansionOptions = new();
        private readonly Dictionary<string, ExpansionOption> _selectedOptions = new();

        public Table()
        {
        }

        public Task<IReadOnlyCollection<Player>> GetPlayers()
        {
            IReadOnlyCollection<Player> players = _players.Values;
            return Task.FromResult(players);
        }

        public Task<JoinTableResult> JoinRoom(Player player)
        {
            if (player is null)
            {
                return Task.FromResult(JoinTableResult.InvalidUser);
            }

            _players[player.Nickname] = player;
            return Task.FromResult(JoinTableResult.JoinedRoom);
        }

        public Task<JoinTableResult> LeaveRoom(Player player)
        {
            if (player is null)
                return Task.FromResult(JoinTableResult.InvalidUser);

            if (!_players.Any())
                return Task.FromResult(JoinTableResult.RoomEmpty);

            if (_players.Remove(player.Nickname))
                return Task.FromResult(JoinTableResult.LeftRoom);

            return Task.FromResult(JoinTableResult.InvalidUser);
        }

        public Task WithExpansions(IReadOnlyCollection<ExpansionOption> expansions)
        {
            if (expansions is null)
            {
                return Task.FromResult<ITable>(this);
            }

            _expansionOptions.Clear();
            _expansionOptions.AddRange(expansions);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyCollection<ExpansionSelection>> GetExpansionSelections()
        {
            IReadOnlyCollection<ExpansionSelection> expansionOptions = _expansionOptions
                .Select(x => new ExpansionSelection(x.Code, x.Title, _selectedOptions.ContainsKey(x.Code)))
                .ToArray();
            return Task.FromResult(expansionOptions);
        }

        public Task<SelectExpansionResult> SelectExpansion(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return Task.FromResult(SelectExpansionResult.InvalidOptionCode);

            var expansion = _expansionOptions.FirstOrDefault(x => string.Equals(x.Code, code, StringComparison.OrdinalIgnoreCase));
            _selectedOptions[code] = expansion;
            return Task.FromResult(SelectExpansionResult.OptionSelected);
        }

        public Task<SelectExpansionResult> UnselectExpansion(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return Task.FromResult(SelectExpansionResult.InvalidOptionCode);

            var expansion = _expansionOptions.FirstOrDefault(x => string.Equals(x.Code, code, StringComparison.OrdinalIgnoreCase));
            _selectedOptions.Remove(code);
            return Task.FromResult(SelectExpansionResult.OptionUnselected);
        }
    }
}

using Munchkin.Runtime.Abstractions.GameRoomAggregate;
using Munchkin.Runtime.Abstractions.UserAggregate;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Entities.GameRoomAggregate
{
    public class GameRoom : Grain, IGameRoom
    {
        private readonly Dictionary<int, User> _players = new();
        private readonly List<ExpansionOption> _expansionOptions = new();
        private readonly Dictionary<string, ExpansionOption> _selectedOptions = new();

        public GameRoom()
        {
        }

        public Task<bool> IsEmpty() => Task.FromResult(_players.Count == 0);

        public Task<IReadOnlyCollection<User>> GetUsers()
        {
            IReadOnlyCollection<User> players = _players.Values;
            return Task.FromResult(players);
        }

        public Task<JoinRoomResult> JoinRoom(User player)
        {
            if (player is null)
            {
                return Task.FromResult(JoinRoomResult.InvalidUser);
            }

            _players[player.UserId] = player;
            return Task.FromResult(JoinRoomResult.JoinedRoom);
        }

        public Task<JoinRoomResult> LeaveRoom(User player)
        {
            if (player is null)
                return Task.FromResult(JoinRoomResult.InvalidUser);

            if (!_players.Any())
                return Task.FromResult(JoinRoomResult.RoomEmpty);

            if (_players.Remove(player.UserId))
                return Task.FromResult(JoinRoomResult.LeftRoom);

            return Task.FromResult(JoinRoomResult.InvalidUser);
        }

        public Task SetAvailableExpansions(ExpansionOption[] avaialableExpansions)
        {
            if (avaialableExpansions is null)
            {
                return Task.FromResult<IGameRoom>(this);
            }

            _expansionOptions.Clear();
            _expansionOptions.AddRange(avaialableExpansions);
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

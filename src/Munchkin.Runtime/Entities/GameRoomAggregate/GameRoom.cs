using Munchkin.Runtime.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Runtime.Entities.GameRoomAggregate
{
    public class GameRoom
    {
        private readonly List<User> _players = new();
        private readonly List<ExpansionOption> _expansionOptions = new();
        private readonly Dictionary<string, ExpansionOption> _selectedOptions = new();

        public GameRoom()
        {
        }

        public bool IsEmpty => _players.Count == 0;

        public IReadOnlyCollection<User> Players => _players;

        public IReadOnlyCollection<ExpansionOption> SelectedExpansions => _selectedOptions.Values;

        public (GameRoom, JoinRoomResult) JoinRoom(User player)
        {
            if (player is null)
            {
                return (this, JoinRoomResult.InvalidUser);
            }

            _players.Add(player);
            return (this, JoinRoomResult.JoinedRoom);
        }

        public (GameRoom, JoinRoomResult) LeaveRoom(User player)
        {
            if (player is null)
                return (this, JoinRoomResult.InvalidUser);

            if (!_players.Any())
                return (this, JoinRoomResult.RoomEmpty);

            if (_players.Remove(player))
                return (this, JoinRoomResult.LeftRoom);

            return (this, JoinRoomResult.InvalidUser);
        }

        public GameRoom SetAvailableExpansions(ExpansionOption[] avaialableExpansions)
        {
            if (avaialableExpansions is null) return this;

            _expansionOptions.Clear();
            _expansionOptions.AddRange(avaialableExpansions);
            return this;
        }

        public (GameRoom, SelectExpansionResult) SelectExpansion(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return (this, SelectExpansionResult.InvalidOptionCode);

            var expansion = _expansionOptions.FirstOrDefault(x => string.Equals(x.Code, code, StringComparison.OrdinalIgnoreCase));
            _selectedOptions[code] = expansion;
            return (this, SelectExpansionResult.OptionSelected);
        }

        public (GameRoom, SelectExpansionResult) UnselectExpansion(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return (this, SelectExpansionResult.InvalidOptionCode);

            var expansion = _expansionOptions.FirstOrDefault(x => string.Equals(x.Code, code, StringComparison.OrdinalIgnoreCase));
            _selectedOptions.Remove(code);
            return (this, SelectExpansionResult.OptionUnselected);
        }
    }
}

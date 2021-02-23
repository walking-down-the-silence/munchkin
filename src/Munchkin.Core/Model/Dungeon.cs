using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.States;
using Munchkin.Core.Model.Stages;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core.Model
{
    /// <summary>
    /// Dungeon state representing all goods in it
    /// </summary>
    public class Dungeon : State
    {
        private readonly Table _table;
        private readonly Dictionary<Player, List<IAction<Table>>> _playerActions = new();

        public Dungeon(Table table)
        {
            _table = table ?? throw new System.ArgumentNullException(nameof(table));
        }

        /// <summary>
        /// Allows main players hero to enter the dungeon
        /// </summary>
        /// <param name="door"> Doors, in which hero should enter the dungeon. </param>
        /// <param name="table"> Table state when entering the dungeon. </param>
        public RoomStage KickOpenTheDoor(DoorsCard door, Table table)
        {
            var playerActions = table.Players.Current.Actions.Select(action => action.Create()).ToList();
            SetPlayerActions(table.Players.Current, playerActions);

            var roomStage = new RoomStage(table, door);
            CurrentStage = roomStage;
            return roomStage;
        }

        public IStage CurrentStage { get; private set; }

        public void SetPlayerActions(Player player, ICollection<IAction<Table>> actions)
        {
            _playerActions[player] = actions.ToList();
        }

        public IReadOnlyCollection<IAction<Table>> GetPlayerActions(Player player)
        {
            return _playerActions[player].AsReadOnly();
        }
    }
}
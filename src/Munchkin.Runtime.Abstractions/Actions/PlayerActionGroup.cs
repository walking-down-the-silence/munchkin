using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using System.Collections.Generic;

namespace Munchkin.Runtime.Entities.Actions
{
    public class PlayerActionGroup
    {
        public PlayerActionGroup(Player player, IReadOnlyCollection<IAction<Table>> actions)
        {
            Player = player ?? throw new System.ArgumentNullException(nameof(player));
            Actions = actions ?? throw new System.ArgumentNullException(nameof(actions));
        }

        public Player Player { get; }

        public IReadOnlyCollection<IAction<Table>> Actions { get; }
    }
}

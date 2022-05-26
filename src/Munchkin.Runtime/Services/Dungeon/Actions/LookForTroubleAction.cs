using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Phases
{
    public sealed class LookForTroubleAction : ActionBase, IDungeonAction
    {
        public LookForTroubleAction(Player player, MonsterCard monster) :
            base(TurnActions.Dungeon.LookForTrouble, "Look For Trouble")
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            Monster = monster ?? throw new ArgumentNullException(nameof(monster));
        }

        public Player Player { get; }
        public MonsterCard Monster { get; }
    }
}
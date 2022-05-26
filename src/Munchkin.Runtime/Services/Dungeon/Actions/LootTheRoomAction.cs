using Munchkin.Core.Contracts.Actions;

namespace Munchkin.Core.Model.Phases
{
    public sealed class LootTheRoomAction : ActionBase, IDungeonAction
    {
        public LootTheRoomAction() :
            base(TurnActions.Dungeon.LootTheRoom, "Loot The Room")
        {
        }
    }
}
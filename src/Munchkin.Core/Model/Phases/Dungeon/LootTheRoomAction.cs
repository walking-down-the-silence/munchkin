using Munchkin.Core.Contracts.Actions;

namespace Munchkin.Core.Model.Phases
{
    public record LootTheRoomAction(Table Table) :
        ActionBase(TurnActions.Dungeon.LootTheRoom, "Loot The Room", string.Empty),
        IDungeonAction;
}
using Munchkin.Core.Contracts.Actions;
using Munchkin.Runtime.Services;

namespace Munchkin.Core.Model.Phases
{
    public record LootTheRoomAction(Table Table) :
        ActionBase(TurnActions.Dungeon.LootTheRoom, "Loot The Room", string.Empty),
        IDungeonAction;
}
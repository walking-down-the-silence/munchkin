using Munchkin.Core.Contracts.Actions;

namespace Munchkin.Core.Model.Phases
{
    public record KickOpenTheDoorAction(Table Table) :
        ActionBase(TurnActions.Dungeon.KickOpenTheDoor, "Kick Open The Door", string.Empty),
        IDungeonAction;
}
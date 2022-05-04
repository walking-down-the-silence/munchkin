using Munchkin.Core.Contracts.Actions;

namespace Munchkin.Core.Model.Phases
{
    public record KickOpenTheDoorAction() :
        ActionBase(TurnActions.Dungeon.KickOpenTheDoor, "Kick Open The Door", string.Empty),
        IDungeonAction;
}
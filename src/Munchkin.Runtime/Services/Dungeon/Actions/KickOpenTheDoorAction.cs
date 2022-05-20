using Munchkin.Core.Contracts.Actions;
using Munchkin.Runtime.Services;

namespace Munchkin.Core.Model.Phases
{
    public record KickOpenTheDoorAction(Table Table) :
        ActionBase(TurnActions.Dungeon.KickOpenTheDoor, "Kick Open The Door", string.Empty),
        IDungeonAction;
}
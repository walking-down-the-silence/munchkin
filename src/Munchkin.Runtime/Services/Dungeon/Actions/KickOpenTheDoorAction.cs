using Munchkin.Core.Contracts.Actions;

namespace Munchkin.Core.Model.Phases
{
    public sealed class KickOpenTheDoorAction : ActionBase, IDungeonAction
    {
        public KickOpenTheDoorAction() :
            base(TurnActions.Dungeon.KickOpenTheDoor, "Kick Open The Door")
        {
        }
    }
}
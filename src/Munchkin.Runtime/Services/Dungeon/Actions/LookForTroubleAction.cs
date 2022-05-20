using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Runtime.Services;

namespace Munchkin.Core.Model.Phases
{
    public record LookForTroubleAction(Player Player, MonsterCard Monster) :
        ActionBase(TurnActions.Dungeon.LookForTrouble, "Look For Trouble", string.Empty),
        IDungeonAction;
}
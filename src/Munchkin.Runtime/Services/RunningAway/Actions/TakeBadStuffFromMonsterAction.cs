using Munchkin.Core.Contracts.Actions;
using Munchkin.Runtime.Services;

namespace Munchkin.Core.Model.Phases
{
    public record TakeBadStuffFromMonsterAction(Table Table, Player Player) :
        ActionBase(TurnActions.RunAway.TakeBadStuff, "Take Bad Stuff", string.Empty),
        IRunningAwayAction;
}
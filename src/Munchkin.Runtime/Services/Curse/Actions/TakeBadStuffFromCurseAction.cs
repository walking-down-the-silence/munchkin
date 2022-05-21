using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Runtime.Services;

namespace Munchkin.Core.Model.Phases
{
    public record TakeBadStuffFromCurseAction(Table Table, CurseCard Curse) :
        ActionBase(TurnActions.Curse.TakeBadStuff, "TakenBy Bad Stuff", string.Empty),
        ICurseAction;
}
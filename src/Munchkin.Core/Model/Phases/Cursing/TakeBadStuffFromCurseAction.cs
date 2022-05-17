using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Phases
{
    public record TakeBadStuffFromCurseAction(Table Table, CurseCard Curse) :
        ActionBase(TurnActions.Curse.TakeBadStuff, "Take Bad Stuff", string.Empty),
        ICurseAction;
}
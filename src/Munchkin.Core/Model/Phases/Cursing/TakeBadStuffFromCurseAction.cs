using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Phases
{
    public record TakeBadStuffFromCurseAction(CurseCard Card) :
        ActionBase(TurnActions.Curse.TakeBadStuffFromCurse, "Take Bad Stuff", string.Empty),
        ICurseAction;
}
using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Phases
{
    public record ResolveAction(Card Card) :
        ActionBase(TurnActions.Curse.Resolve, "Resolve The Curse", string.Empty),
        ICurseAction;
}
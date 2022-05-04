using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Phases
{
    public record ResolveWhileInCombatAction(Card Card) :
        ActionBase(TurnActions.Curse.ResolveWhileInCombat, "Resolve The Curse", string.Empty),
        ICurseAction;
}
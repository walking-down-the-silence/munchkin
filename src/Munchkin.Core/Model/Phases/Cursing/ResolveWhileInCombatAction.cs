using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Phases
{
    public record ResolveWhileInCombatAction(Card Card) : ICurseAction;
}
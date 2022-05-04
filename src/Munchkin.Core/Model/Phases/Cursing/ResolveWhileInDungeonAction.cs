using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Phases
{
    public record ResolveWhileInDungeonAction(Card Card) :
        ActionBase(TurnActions.Curse.ResolveWhileInDungeon, "Resolve The Curse", string.Empty),
        ICurseAction;
}
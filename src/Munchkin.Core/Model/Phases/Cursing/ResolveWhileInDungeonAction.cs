using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Phases
{
    public record ResolveWhileInDungeonAction(Card Card) : ICurseAction;
}
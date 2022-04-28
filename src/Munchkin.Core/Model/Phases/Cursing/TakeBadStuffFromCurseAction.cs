using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Phases
{
    public record TakeBadStuffFromCurseAction(
        CurseCard Card) : ICurseAction;
}
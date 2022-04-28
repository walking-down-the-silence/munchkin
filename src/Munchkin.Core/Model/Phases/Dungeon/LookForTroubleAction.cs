using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Phases
{
    public record LookForTroubleAction(
        Player Player,
        MonsterCard Monster) : IDungeonAction;
}
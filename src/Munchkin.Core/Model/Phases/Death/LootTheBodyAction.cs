using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Phases
{
    public record LootTheBodyAction(Player Player, Card Card) :
        ActionBase(TurnActions.Death.LootTheBody, "Loot The Body", string.Empty),
        IDeathAction;
}

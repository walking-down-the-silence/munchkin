using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Runtime.Services;

namespace Munchkin.Core.Model.Phases
{
    public record LootTheBodyAction(Table Table, Player Player, Card Card) :
        ActionBase(TurnActions.Death.LootTheBody, "Loot The Body", string.Empty),
        IDeathAction;
}

using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using System.Collections.Generic;

namespace Munchkin.Runtime.Services
{
    public record LootTheBodyOptions(
        Player BodyToLoot,
        IReadOnlyCollection<Player> TakenBy,
        IReadOnlyCollection<Card> CardsToLoot);
}

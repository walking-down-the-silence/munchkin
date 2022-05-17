using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using System.Collections.Generic;

namespace Munchkin.Runtime.Services
{
    public record CharityOptions(Player Player, IReadOnlyCollection<Card> CardsToChooseFrom);
}

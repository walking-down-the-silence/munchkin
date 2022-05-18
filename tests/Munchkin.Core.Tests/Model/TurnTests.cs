using FluentAssertions;
using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using System.Collections.Generic;
using Xunit;

namespace Munchkin.Core.Tests.Model
{
    public class TurnTests
    {
        [Fact]
        public void Next_ShouldChangeCurrentPlayer()
        {
            // Arrange
            var player1 = new Player("johny.cash", EGender.Male);
            var player2 = new Player("frank.sinatra", EGender.Male);
            var table = Table.Empty()
                .WithWinningLevel(10)
                .WithDoorDeck(GetAllDoorsCardsRandomlyShuffled())
                .WithTreasureDeck(GetAllTreasuresCardsRandomlyShuffled());
            var joined1 = table.Join(player1);
            var joined2 = table.Join(player2);

            // Act
            var currentTurnPlayer = table.Players.Current;
            var nextTable = table.NextTurn();
            var nextTurnPlayer = table.Players.Current;

            // Assert
            currentTurnPlayer.Should().NotBeSameAs(nextTurnPlayer);
            currentTurnPlayer.Should().BeSameAs(player1);
            nextTurnPlayer.Should().BeSameAs(player2);
        }

        private static IReadOnlyCollection<DoorsCard> GetAllDoorsCardsRandomlyShuffled()
        {
            return new MunchkinOriginalDoorsFactory().GetDoorsCards();
        }

        public static IReadOnlyCollection<TreasureCard> GetAllTreasuresCardsRandomlyShuffled()
        {
            return new MunchkinOriginalTreasuresFactory().GetTreasureCards();
        }
    }
}

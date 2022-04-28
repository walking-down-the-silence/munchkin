using FluentAssertions;
using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using System;
using System.Collections.Generic;
using Xunit;

namespace Munchkin.Core.Tests.Model
{
    public class TurnTests
    {
        [Fact]
        public void From_WithNullParameter_ShouldThrowArgumentNullExcpetion()
        {
            // Act
            var exception = Record.Exception(() => Turn.From(null));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public void From_WithNotNullParameter_ShouldNotThrowAnException()
        {
            // Arrange
            var player = new Player("johny.cash", EGender.Male);
            var table = Table.Empty()
                .WithWinningLevel(10)
                .WithDoorDeck(GetAllDoorsCardsRandomlyShuffled())
                .WithTreasureDeck(GetAllTreasuresCardsRandomlyShuffled());
            var joined = table.Join(player);

            // Act
            var exception = Record.Exception(() => Turn.From(table));

            // Assert
            exception.Should().BeNull();
        }

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
            var currentTurn = Turn.From(table);
            var currentTurnPlayer = table.Players.Current;
            var nextTurn = Turn.Next(currentTurn);
            var nextTurnPlayer = table.Players.Current;

            // Assert
            currentTurnPlayer.Should().NotBeSameAs(nextTurnPlayer);
            currentTurnPlayer.Should().BeSameAs(player1);
            nextTurnPlayer.Should().BeSameAs(player2);
            nextTurn.Should().NotBeSameAs(currentTurn);
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

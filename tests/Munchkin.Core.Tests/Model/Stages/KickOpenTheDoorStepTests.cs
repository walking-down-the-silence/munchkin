using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using Munchkin.Core.Model.Stages;
using Munchkin.Engine.Original.Doors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Munchkin.Core.Tests.Model.Stages
{
    public class KickOpenTheDoorStepTests
    {
        [Fact]
        public void Ctor_WithNullParameter_SholdThrowArgumentNullExpection()
        {
            // Act
            Player player = null;

            // Act
            var exception = Record.Exception(() => new KickOpenTheDoorStep(player));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
            Assert.Equal("Value cannot be null. (Parameter 'currentPlayer')", exception.Message);
        }

        [Fact]
        public void Ctor_WithNotNullParameter_SholdNotThrowArgumentNullExpection()
        {
            // Arrange
            var player = CreatePlayerJohny();

            // Act
            var exception = Record.Exception(() => new KickOpenTheDoorStep(player));

            // Act, Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task OnResolve_WhenCardIsNotAMonsterAndIsNotACurse_ShouldTakeCardInHand()
        {
            // Arrange
            var player = CreatePlayerJohny();
            var card = new DwarfRace();
            var doorsCards = new List<DoorsCard>() { card };
            var players = new List<Player>() { player };

            var step = new KickOpenTheDoorStep(player);
            
            var emptyTable = Table.Empty();
            var table = emptyTable.
                WithDoorDeck(doorsCards).
                WithPlayers(players);

            // Act
            var result = await step.Resolve(table);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(card, table.Players.Current.YourHand);
        }

        [Fact]
        public async Task OnResolve_WhenCardIsAMonsterAndIsNotACurse_ShouldPutInPlay()
        {
            // Arrange
            var player = CreatePlayerJohny();
            var card = new BandOf3872Orcs();
            var doorsCards = new List<DoorsCard>() { card };
            var players = new List<Player>() { player };

            var step = new KickOpenTheDoorStep(player);

            var emptyTable = Table.Empty();
            var table = emptyTable.
                WithDoorDeck(doorsCards).
                WithPlayers(players);

            // Act
            var result = await step.Resolve(table);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(card, table.Dungeon.PlayedCards);
        }

        [Fact]
        public async Task OnResolve_WhenCardIsNotAMonsterButIsACurse_ShouldTakeConsequences()
        {
            // Arrange
            var player = CreatePlayerJohny();
            var card = new ChangeSex();
            var doorsCards = new List<DoorsCard>() { card };
            var players = new List<Player>() { player };

            var step = new KickOpenTheDoorStep(player);

            var emptyTable = Table.Empty();
            var table = emptyTable.
                WithDoorDeck(doorsCards).
                WithPlayers(players);

            // Act
            var result = await step.Resolve(table);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(card, table.Dungeon.PlayedCards);
        }

        private static Player CreatePlayerJohny()
        {
            return new Player("Johny Cash", EGender.Male);
        }
    }
}

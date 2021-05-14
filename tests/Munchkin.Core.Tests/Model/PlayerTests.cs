﻿using MediatR;
using Moq;
using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using Munchkin.Engine.Original.Doors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Munchkin.Core.Tests.Model
{
    public class PlayerTests
    {
        [Fact]
        public void Ctor_WithWhitespaceName_ThorwsArgumentException()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() => new Player(string.Empty, EGender.Male));
        }

        [Fact]
        public void Ctor_WithValidName_ShouldCreatePlayer()
        {
            // Arrange
            var player = CreatePlayerJohny();

            // Act, Assert
            Assert.NotNull(player.Name);
            Assert.Equal(EGender.Male, player.Gender);
            Assert.Equal(1, player.Level);
            Assert.False(player.IsDead);
            Assert.Empty(player.YourHand);
            Assert.Empty(player.Equipped);
            Assert.Empty(player.Backpack);
        }

        [Fact]
        public void TakeInHand_WithNull_ShouldHaveEmptyHand()
        {
            // Arrange
            var player = CreatePlayerJohny();

            // Act
            player.TakeInHand(null);

            // Assert
            Assert.Empty(player.YourHand);
            Assert.Empty(player.Equipped);
            Assert.Empty(player.Backpack);
        }

        [Fact]
        public void TakeInHand_WithValidCard_ShouldNotHaveEmptyHand()
        {
            // Arrange
            var player = CreatePlayerJohny();
            var card = new SuperMunchkin();

            // Act
            player.TakeInHand(card);

            // Assert
            Assert.NotEmpty(player.YourHand);
            Assert.Empty(player.Equipped);
            Assert.Empty(player.Backpack);
        }

        [Fact]
        public void PutInPlayAsEquipped_WithNull_ShouldNotHaveEmptyEquipped()
        {
            // Arrange
            var player = CreatePlayerJohny();

            // Act
            player.Equip(null);

            // Assert
            Assert.Empty(player.YourHand);
            Assert.Empty(player.Equipped);
            Assert.Empty(player.Backpack);
        }

        [Fact]
        public void PutInPlayAsEquipped_WithValidCard_ShouldNotHaveEmptyEquipped()
        {
            // Arrange
            var player = CreatePlayerJohny();
            var card = new SuperMunchkin();

            // Act
            player.TakeInHand(card);
            player.Equip(card);

            // Assert
            Assert.Empty(player.YourHand);
            Assert.NotEmpty(player.Equipped);
            Assert.Empty(player.Backpack);
        }

        [Fact]
        public void PutInPlayAsCarried_WithNull_ShouldNotHaveEmptyBackpack()
        {
            // Arrange
            var player = CreatePlayerJohny();

            // Act
            player.PutInBackpack(null);

            // Assert
            Assert.Empty(player.YourHand);
            Assert.Empty(player.Equipped);
            Assert.Empty(player.Backpack);
        }

        [Fact]
        public void PutInPlayAsCarried_WithValidCard_ShouldNotHaveEmptyBackpack()
        {
            // Arrange
            var player = CreatePlayerJohny();
            var card = new SuperMunchkin();

            // Act
            player.TakeInHand(card);
            player.PutInBackpack(card);

            // Assert
            Assert.Empty(player.YourHand);
            Assert.Empty(player.Equipped);
            Assert.NotEmpty(player.Backpack);
        }

        [Fact]
        public void LevelDown_ShouldNotGoLowerThanLevel1()
        {
            // Arrange
            var player = CreatePlayerJohny();

            // Act
            player.LevelDown();
            player.LevelDown();
            player.LevelDown();

            // Assert
            Assert.Equal(1, player.Level);
        }

        [Fact]
        public void LevelUp_ShouldGoUpOneLevel()
        {
            // Arrange
            var player = CreatePlayerJohny();

            // Act
            player.LevelUp();

            // Assert
            Assert.Equal(2, player.Level);
        }

        [Fact]
        public async void Discard_WithValidCard_ShouldHaveNotEmptyDoorsDiscardPile()
        {
            // Arrange
            var player = CreatePlayerJohny();
            var players = new[] { player };
            var treasureFactory = new MunchkinOriginalTreasuresFactory();
            var doorFactory = new MunchkinOriginalDoorsFactory();
            var table = await SetupTable(players, treasureFactory, doorFactory, 10);

            // Act
            var card = player.YourHand.OfType<DoorsCard>().First();
            player.Discard(table, card);

            // Assert
            Assert.Empty(player.Equipped);
            Assert.Empty(player.Backpack);
            Assert.Equal(7, player.YourHand.Count);
            Assert.NotEmpty(table.DiscardedDoorsCards);
        }

        [Fact]
        public async void DiscardHand_WithTable_ShouldHaveEmptyHandAndNotEmptyDiscardPiles()
        {
            // Arrange
            var player = CreatePlayerJohny();
            var players = new[] { player };
            var treasureFactory = new MunchkinOriginalTreasuresFactory();
            var doorFactory = new MunchkinOriginalDoorsFactory();
            var table = await SetupTable(players, treasureFactory, doorFactory, 10);

            // Act
            player.DiscardHand(table);

            // Assert
            Assert.Empty(player.Equipped);
            Assert.Empty(player.Backpack);
            Assert.Empty(player.YourHand);
            Assert.NotEmpty(table.DiscardedTreasureCards);
            Assert.NotEmpty(table.DiscardedDoorsCards);
        }

        [Fact]
        public async void DiscardEquipped_WithTable_ShouldHaveEmptyHandAndNotEmptyDiscardPiles()
        {
            // Arrange
            var player = CreatePlayerJohny();
            var players = new[] { player };
            var treasureFactory = new MunchkinOriginalTreasuresFactory();
            var doorFactory = new MunchkinOriginalDoorsFactory();
            var table = await SetupTable(players, treasureFactory, doorFactory, 10);

            // Act
            var treasureCards = player.YourHand.OfType<TreasureCard>().ToList();
            treasureCards.ForEach(card => player.Equip(card));
            player.DiscardEquipped(table);

            // Assert
            Assert.Empty(player.Equipped);
            Assert.Empty(player.Backpack);
            Assert.NotEmpty(player.YourHand);
            Assert.NotEmpty(table.DiscardedTreasureCards);
        }

        [Fact]
        public async void PutInPlayAsCarried_WithCardFromHand_ShouldHaveNotEmptyBackpack()
        {
            // Arrange
            var player = CreatePlayerJohny();
            var players = new[] { player };
            var treasureFactory = new MunchkinOriginalTreasuresFactory();
            var doorFactory = new MunchkinOriginalDoorsFactory();
            var table = await SetupTable(players, treasureFactory, doorFactory, 10);

            // Act
            var card = player.YourHand.OfType<TreasureCard>().First();
            player.PutInBackpack(card);

            // Assert
            Assert.Empty(player.Equipped);
            Assert.NotEmpty(player.Backpack);
            Assert.Equal(7, player.YourHand.Count);
        }

        [Fact]
        public async void PutInPlayAsEquipped_WithCardFromHand_ShouldHaveNotEmptyEquipped()
        {
            // Arrange
            var player = CreatePlayerJohny();
            var players = new[] { player };
            var treasureFactory = new MunchkinOriginalTreasuresFactory();
            var doorFactory = new MunchkinOriginalDoorsFactory();
            var table = await SetupTable(players, treasureFactory, doorFactory, 10);

            // Act
            var card = player.YourHand.OfType<TreasureCard>().First();
            player.Equip(card);

            // Assert
            Assert.NotEmpty(player.Equipped);
            Assert.Empty(player.Backpack);
            Assert.Equal(7, player.YourHand.Count);
        }

        [Fact]
        public async void Revive_WithTable_ShouldHaveNotEmptyHand()
        {
            // Arrange
            var player = CreatePlayerJohny();
            var players = new[] { player };
            var treasureFactory = new MunchkinOriginalTreasuresFactory();
            var doorFactory = new MunchkinOriginalDoorsFactory();
            var table = await SetupTableNoRevive(players, treasureFactory, doorFactory, 10);

            // Act
            player.Revive(table);

            // Assert
            Assert.NotEmpty(player.YourHand);
            Assert.Equal(8, player.YourHand.Count);
            Assert.False(player.IsDead);
        }

        [Fact]
        public async void Kill_ShouldHaveEmptyHand_ButRemainRaceAndClassAndSuperMunchkinAndHalfbreedAndCurses()
        {
            // Arrange
            var player = CreatePlayerJohny();
            var players = new[] { player };
            var treasureFactory = new MunchkinOriginalTreasuresFactory();
            var doorFactory = new MunchkinOriginalDoorsFactory();
            var table = await SetupTable(players, treasureFactory, doorFactory, 10);

            // Act
            player.Equip(new ElfRace());
            player.Equip(new WarriorClass());
            player.Equip(new SuperMunchkin());
            player.Equip(new Halfbreed());
            await player.Kill(table);

            // Assert
            Assert.Empty(player.YourHand);
            Assert.True(player.IsDead);
            Assert.NotEmpty(player.Equipped.OfType<RaceCard>());
            Assert.NotEmpty(player.Equipped.OfType<ClassCard>());
            Assert.NotEmpty(player.Equipped.OfType<SuperMunchkin>());
            Assert.NotEmpty(player.Equipped.OfType<Halfbreed>());
        }

        private static Player CreatePlayerJohny()
        {
            return new Player("Johny Cash", EGender.Male);
        }

        private async Task<Table> SetupTable(
            IEnumerable<Player> players,
            ITreasureDeckFactory treasureFactory,
            IDoorDeckFactory doorFactory,
            int winningLevel)
        {
            var table = Table.Empty();
            table = await table.WithWinningLevel(winningLevel);

            if (treasureFactory != null)
            {
                table = await table.WithTreasureDeck(treasureFactory.GetTreasureCards().ToArray());
            }

            if (doorFactory != null)
            {
                table = await table.WithDoorDeck(doorFactory.GetDoorsCards().ToArray());
            }

            table = await table.WithPlayers(players.ToArray());
            table.Players.ForEach(player => player.Revive(table));

            return table;
        }

        private async Task<Table> SetupTableNoRevive(
            IEnumerable<Player> players,
            ITreasureDeckFactory treasureFactory,
            IDoorDeckFactory doorFactory,
            int winningLevel)
        {
            var table = Table.Empty();
            table = await table.WithWinningLevel(winningLevel);

            if (treasureFactory != null)
            {
                table = await table.WithTreasureDeck(treasureFactory.GetTreasureCards().ToArray());
            }

            if (doorFactory != null)
            {
                table = await table.WithDoorDeck(doorFactory.GetDoorsCards().ToArray());
            }

            table = await table.WithPlayers(players.ToArray());

            return table;
        }
    }
}

﻿using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards.Doors.Classes;
using Munchkin.Core.Model.Cards.Doors.Enhancers;
using Munchkin.Core.Model.Cards.Doors.Monsters;
using Munchkin.Core.Model.Cards.Treasures.OneShot;
using Munchkin.Core.Model.Cards.Treasures.Permanent;
using Munchkin.Core.Model.Expansions;
using Munchkin.Primitives.Abstractions;
using Munchkin.Runtime.Services;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Munchkin.Runtime.Tests
{
    public class ProgramTests
    {
        private const string PlayerJohnyCashNickname = "johny.cash";
        private const string PlayerFrankSinatraNickname = "frank.sinatra";
        private const string PlayerElonMuskNickname = "elon.musk";
        private const string PlayerPeterJacksonNickname = "peter.jackson";

        [Fact]
        public async Task TestRun()
        {
            // Arrange
            var serviceCollection = new ServiceCollection()
                .AddMunchkinDeluxeTest()
                .AddMunchkinInMemoryPersistance()
                .AddMunchkinGameServices();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Act
            var table = await TableSetup_PlayersJoin(serviceProvider);
            table = await Turn1_JohnyCash_EmptyRoom(serviceProvider, table);
            table = await Turn2_FrankSinatra_Combat(serviceProvider, table);
            table = await Turn3_ElonMusk_Curse(serviceProvider, table);

            // Assert
            table.Should().NotBeNull();
        }

        private static async Task<Table> TableSetup_PlayersJoin(ServiceProvider serviceProvider)
        {
            var tableService = serviceProvider.GetRequiredService<TableService>();
            var playerService = serviceProvider.GetRequiredService<PlayerService>();

            // NOTE: players register / online
            var playerJohny = await playerService.CreatePlayerAsync(PlayerJohnyCashNickname, true);
            var playerFrank = await playerService.CreatePlayerAsync(PlayerFrankSinatraNickname, true);
            var playerElon = await playerService.CreatePlayerAsync(PlayerElonMuskNickname, true);
            var playerPeter = await playerService.CreatePlayerAsync(PlayerPeterJacksonNickname, true);

            playerJohny.Should().NotBeNull();
            playerFrank.Should().NotBeNull();
            playerElon.Should().NotBeNull();
            playerPeter.Should().NotBeNull();

            // NOTE: a player starts the game
            var table = await tableService.CreateAsync(new NonShuffleAlgorithm<Card>());

            table.Should().NotBeNull();

            // NOTE: more players join the game table
            var joined1 = await tableService.JoinTableAsync(TableService.GetUniqueId(table), PlayerJohnyCashNickname);
            var joined2 = await tableService.JoinTableAsync(TableService.GetUniqueId(table), PlayerFrankSinatraNickname);
            var joined3 = await tableService.JoinTableAsync(TableService.GetUniqueId(table), PlayerElonMuskNickname);

            joined1.Should().NotBeNull();
            joined1.Should().BeEquivalentTo(JoinTableResult.Joined);
            joined2.Should().NotBeNull();
            joined2.Should().BeEquivalentTo(JoinTableResult.Joined);
            joined3.Should().NotBeNull();
            joined3.Should().BeEquivalentTo(JoinTableResult.Joined);

            table = await tableService.SetupAsync(TableService.GetUniqueId(table));

            table.Should().NotBeNull();
            table.Players.Count.Should().Be(3);

            // NOTE: try to join after the game has started
            var joined4 = await tableService.JoinTableAsync(TableService.GetUniqueId(table), PlayerPeterJacksonNickname);

            joined4.Should().NotBeNull();
            joined4.Should().BeEquivalentTo(JoinTableResult.Full);

            return table;
        }

        private static async Task<Table> Turn1_JohnyCash_EmptyRoom(ServiceProvider serviceProvider, Table table)
        {
            var tableService = serviceProvider.GetRequiredService<TableService>();
            var dungeonService = serviceProvider.GetRequiredService<DungeonService>();

            table.Should().NotBeNull();
            table.Players.Current.Should().NotBeNull();
            table.Players.Current.Nickname.Should().BeEquivalentTo(PlayerJohnyCashNickname);

            // PHASE: Kick Open The Door
            table = await dungeonService.KickOpenTheDoorAsync(TableService.GetUniqueId(table));

            // PHASE: Loot The Room / Look For Trouble
            table = await dungeonService.LootTheRoomAsync(TableService.GetUniqueId(table));

            table.Players.Current.YourHand.Should().HaveCount(10);

            // PHASE: Charity
            string johnyCardEquipped1Id = table.Players.Current.FirstOrDefault<ClericClass>().Code;
            string johnyCardEquipped2Id = table.Players.Current.FirstOrDefault<SneakyBastardSword>().Code;
            table = await tableService.EquipAsync(TableService.GetUniqueId(table), PlayerJohnyCashNickname, johnyCardEquipped1Id);
            table = await tableService.EquipAsync(TableService.GetUniqueId(table), PlayerJohnyCashNickname, johnyCardEquipped2Id);

            string johnyCardDiscarded1Id = table.Players.Current.FirstOrDefault<GelatinousOctahedron>().Code;
            string johnyCardDiscarded2Id = table.Players.Current.FirstOrDefault<Ancient>().Code;
            string johnyCardDiscarded3Id = table.Players.Current.FirstOrDefault<ElevenFootPole>().Code;
            table = await tableService.DiscardAsync(TableService.GetUniqueId(table), johnyCardDiscarded1Id);
            table = await tableService.DiscardAsync(TableService.GetUniqueId(table), johnyCardDiscarded2Id);
            table = await tableService.DiscardAsync(TableService.GetUniqueId(table), johnyCardDiscarded3Id);

            table.Players.Current.YourHand.Should().HaveCount(5);
            table.Players.Current.Equipped.Should().HaveCount(2);
            table.DiscardedDoorsCards.Should().HaveCount(2);
            table.DiscardedTreasureCards.Should().HaveCount(1);

            return table;
        }

        private static async Task<Table> Turn2_FrankSinatra_Combat(ServiceProvider serviceProvider, Table table)
        {
            var tableService = serviceProvider.GetRequiredService<TableService>();
            var dungeonService = serviceProvider.GetRequiredService<DungeonService>();
            var charityService = serviceProvider.GetRequiredService<CharityService>();
            var combatService = serviceProvider.GetRequiredService<CombatService>();

            table = await tableService.NextAsync(TableService.GetUniqueId(table));

            table.Should().NotBeNull();
            table.Players.Current.Should().NotBeNull();
            table.Players.Current.Nickname.Should().BeEquivalentTo(PlayerFrankSinatraNickname);

            // PHASE: Play Cards
            string frankCardEquipped1Id = table.Players.Current.FirstOrDefault<ThousandGoldPieces>().Code;
            string frankCardEquipped2Id = table.Players.Current.FirstOrDefault<MutilateTheBodies>().Code;
            table = await tableService.PlayAsync(TableService.GetUniqueId(table), PlayerFrankSinatraNickname, frankCardEquipped1Id);
            table = await tableService.PlayAsync(TableService.GetUniqueId(table), PlayerFrankSinatraNickname, frankCardEquipped2Id);

            // PHASE: Kick Open The Door
            table = await dungeonService.KickOpenTheDoorAsync(TableService.GetUniqueId(table));

            // PHASE: Loot The Room / Look For Trouble
            string frankMonsterFromHand1Id = table.Players.Current.FirstOrDefault<PottedPlant>().Code;
            table = await dungeonService.LookForTroubleAsync(TableService.GetUniqueId(table), frankMonsterFromHand1Id);
            table = await combatService.RewardAsync(TableService.GetUniqueId(table));

            table.Players.Current.Level.Should().Be(4);
            table.Players.Current.YourHand.Should().HaveCount(7);
            table.DiscardedDoorsCards.Should().HaveCount(2);
            table.DungeonCards.Should().HaveCount(3);

            // PHASE: Charity
            string frankCardGiven1Id = table.Players.Current.FirstOrDefault<BoilAnAnthill>().Code;
            string frankCardGiven2Id = table.Players.Current.FirstOrDefault<WishingRing>().Code;
            table = await charityService.GiveAwayAsync(TableService.GetUniqueId(table), frankCardGiven1Id, PlayerFrankSinatraNickname, PlayerElonMuskNickname);
            table = await charityService.GiveAwayAsync(TableService.GetUniqueId(table), frankCardGiven2Id, PlayerFrankSinatraNickname, PlayerElonMuskNickname);

            table.Players.Current.YourHand.Should().HaveCount(5);
            table.Players.First(x => x.Nickname == PlayerElonMuskNickname).Backpack.Should().HaveCount(2);

            return table;
        }

        private static async Task<Table> Turn3_ElonMusk_Curse(ServiceProvider serviceProvider, Table table)
        {
            var tableService = serviceProvider.GetRequiredService<TableService>();
            var dungeonService = serviceProvider.GetRequiredService<DungeonService>();
            var charityService = serviceProvider.GetRequiredService<CharityService>();
            var curseService = serviceProvider.GetRequiredService<CurseService>();

            table = await tableService.NextAsync(TableService.GetUniqueId(table));

            table.Should().NotBeNull();
            table.Players.Current.Should().NotBeNull();
            table.Players.Current.Nickname.Should().BeEquivalentTo(PlayerElonMuskNickname);

            // PHASE: Play Cards
            string elonCardEquipped1Id = table.Players.Current.FirstOrDefault<DaggerOfTreachery>().Code;
            string elonCardEquipped2Id = table.Players.Current.FirstOrDefault<CloakOfObscurity>().Code;
            table = await tableService.EquipAsync(TableService.GetUniqueId(table), PlayerFrankSinatraNickname, elonCardEquipped1Id);
            table = await tableService.EquipAsync(TableService.GetUniqueId(table), PlayerFrankSinatraNickname, elonCardEquipped2Id);

            // PHASE: Kick Open The Door
            table = await dungeonService.KickOpenTheDoorAsync(TableService.GetUniqueId(table));

            // PHASE: Loot The Room / Look For Trouble
            string elonWishingRing1Id = table.Players.Current.FirstOrDefault<WishingRing>().Code;
            string frankCurse1Id = table.Players.First(x => x.Nickname == PlayerFrankSinatraNickname).FirstOrDefault<WishingRing>().Code;
            table = await curseService.ResolveAsync(TableService.GetUniqueId(table), elonWishingRing1Id);
            table = await tableService.CursePlayerAsync(TableService.GetUniqueId(table), elonWishingRing1Id, frankCurse1Id);
            table = await curseService.TakeBadStuffAsync(TableService.GetUniqueId(table), frankCurse1Id);

            table.Players.Current.Level.Should().Be(4);
            table.Players.Current.YourHand.Should().HaveCount(7);
            table.DiscardedDoorsCards.Should().HaveCount(2);
            table.DungeonCards.Should().HaveCount(3);

            // PHASE: Charity
            string elonCardGiven1Id = table.Players.Current.FirstOrDefault<BoilAnAnthill>().Code;
            string elonCardGiven2Id = table.Players.Current.FirstOrDefault<WishingRing>().Code;
            table = await charityService.GiveAwayAsync(TableService.GetUniqueId(table), elonCardGiven1Id, PlayerFrankSinatraNickname, PlayerElonMuskNickname);
            table = await charityService.GiveAwayAsync(TableService.GetUniqueId(table), elonCardGiven2Id, PlayerFrankSinatraNickname, PlayerElonMuskNickname);

            table.Players.Current.YourHand.Should().HaveCount(5);
            table.Players.First(x => x.Nickname == PlayerElonMuskNickname).Backpack.Should().HaveCount(2);

            return table;
        }

        private class NonShuffleAlgorithm<T> : IShuffleAlgorithm<T>
        {
            public void Shuffle(T[] array) { }
        }
    }
}

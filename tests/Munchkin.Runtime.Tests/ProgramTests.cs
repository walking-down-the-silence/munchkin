using FluentAssertions;
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

            // Act / Assert
            var table = await TableSetup_PlayersJoin(serviceProvider);
            table = await Turn1_JohnyCash_EmptyRoom(serviceProvider, table);
            table = await Turn2_FrankSinatra_Combat(serviceProvider, table);
            table = await Turn3_ElonMusk_Curse(serviceProvider, table);
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
            string tableId = TableService.GetUniqueId(table);

            table.Should().NotBeNull();

            // NOTE: more players join the game table
            var joined1 = await tableService.JoinTableAsync(tableId, PlayerJohnyCashNickname);
            var joined2 = await tableService.JoinTableAsync(tableId, PlayerFrankSinatraNickname);
            var joined3 = await tableService.JoinTableAsync(tableId, PlayerElonMuskNickname);

            joined1.Should().NotBeNull();
            joined1.Should().BeEquivalentTo(JoinTableResult.Joined);
            joined2.Should().NotBeNull();
            joined2.Should().BeEquivalentTo(JoinTableResult.Joined);
            joined3.Should().NotBeNull();
            joined3.Should().BeEquivalentTo(JoinTableResult.Joined);

            table = await tableService.SetupAsync(tableId);
            tableId = TableService.GetUniqueId(table);

            table.Should().NotBeNull();
            table.Players.Count.Should().Be(3);

            // NOTE: try to join after the game has started
            var joined4 = await tableService.JoinTableAsync(tableId, PlayerPeterJacksonNickname);

            joined4.Should().NotBeNull();
            joined4.Should().BeEquivalentTo(JoinTableResult.Full);

            return table;
        }

        private static async Task<Table> Turn1_JohnyCash_EmptyRoom(ServiceProvider serviceProvider, Table table)
        {
            var tableService = serviceProvider.GetRequiredService<TableService>();
            var dungeonService = serviceProvider.GetRequiredService<DungeonService>();

            string tableId = TableService.GetUniqueId(table);

            table.Should().NotBeNull();
            table.Turns.Current.Player.Should().NotBeNull();
            table.Turns.Current.Player.Nickname.Should().BeEquivalentTo(PlayerJohnyCashNickname);

            // PHASE: Kick Open The Door
            await dungeonService.KickOpenTheDoorAsync(tableId);

            // PHASE: Loot The Room / Look For Trouble
            await dungeonService.LootTheRoomAsync(tableId);

            table.Turns.Current.Player.YourHand.Should().HaveCount(10);

            // PHASE: Charity
            string johnyCardEquipped1Id = CardService.GetUniqueId(table.Turns.Current.Player.FirstOrDefault<ClericClass>());
            string johnyCardEquipped2Id = CardService.GetUniqueId(table.Turns.Current.Player.FirstOrDefault<SneakyBastardSword>());
            await tableService.EquipAsync(tableId, PlayerJohnyCashNickname, johnyCardEquipped1Id);
            await tableService.EquipAsync(tableId, PlayerJohnyCashNickname, johnyCardEquipped2Id);

            string johnyCardDiscarded1Id = CardService.GetUniqueId(table.Turns.Current.Player.FirstOrDefault<GelatinousOctahedron>());
            string johnyCardDiscarded2Id = CardService.GetUniqueId(table.Turns.Current.Player.FirstOrDefault<Ancient>());
            string johnyCardDiscarded3Id = CardService.GetUniqueId(table.Turns.Current.Player.FirstOrDefault<ElevenFootPole>());
            await tableService.DiscardAsync(tableId, johnyCardDiscarded1Id);
            await tableService.DiscardAsync(tableId, johnyCardDiscarded2Id);
            await tableService.DiscardAsync(tableId, johnyCardDiscarded3Id);

            table.Turns.Current.Player.YourHand.Should().HaveCount(5);
            table.Turns.Current.Player.Equipped.Should().HaveCount(2);
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

            string tableId = TableService.GetUniqueId(table);
            table = await tableService.NextAsync(tableId);

            table.Should().NotBeNull();
            table.Turns.Current.Player.Should().NotBeNull();
            table.Turns.Current.Player.Nickname.Should().BeEquivalentTo(PlayerFrankSinatraNickname);

            // PHASE: Play Cards
            string frankCardEquipped1Id = CardService.GetUniqueId(table.Turns.Current.Player.FirstOrDefault<ThousandGoldPieces>());
            string frankCardEquipped2Id = CardService.GetUniqueId(table.Turns.Current.Player.FirstOrDefault<MutilateTheBodies>());
            await tableService.PlayAsync(tableId, PlayerFrankSinatraNickname, frankCardEquipped1Id);
            await tableService.PlayAsync(tableId, PlayerFrankSinatraNickname, frankCardEquipped2Id);

            // PHASE: Kick Open The Door
            await dungeonService.KickOpenTheDoorAsync(tableId);

            // PHASE: Loot The Room / Look For Trouble
            string frankMonsterFromHand1Id = CardService.GetUniqueId(table.Turns.Current.Player.FirstOrDefault<PottedPlant>());
            await dungeonService.LookForTroubleAsync(tableId, frankMonsterFromHand1Id);
            await combatService.RewardAsync(tableId);

            table.Turns.Current.Player.Level.Should().Be(4);
            table.DiscardedDoorsCards.Should().HaveCount(4);

            // PHASE: Charity
            string frankCardGiven1Id = CardService.GetUniqueId(table.Turns.Current.Player.FirstOrDefault<BoilAnAnthill>());
            string frankCardGiven2Id = CardService.GetUniqueId(table.Turns.Current.Player.FirstOrDefault<WishingRing>());
            await charityService.GiveAwayAsync(tableId, frankCardGiven1Id, PlayerFrankSinatraNickname, PlayerElonMuskNickname);
            await charityService.GiveAwayAsync(tableId, frankCardGiven2Id, PlayerFrankSinatraNickname, PlayerElonMuskNickname);

            return table;
        }

        private static async Task<Table> Turn3_ElonMusk_Curse(ServiceProvider serviceProvider, Table table)
        {
            var tableService = serviceProvider.GetRequiredService<TableService>();

            string tableId = TableService.GetUniqueId(table);
            table = await tableService.NextAsync(tableId);

            table.Should().NotBeNull();
            table.Turns.Current.Player.Should().NotBeNull();
            table.Turns.Current.Player.Nickname.Should().BeEquivalentTo(PlayerElonMuskNickname);

            return table;
        }

        private class NonShuffleAlgorithm<T> : IShuffleAlgorithm<T>
        {
            public void Shuffle(T[] array) { }
        }
    }
}

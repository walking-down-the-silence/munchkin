using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Cards.Doors.Classes;
using Munchkin.Core.Model.Cards.Treasures.OneShot;
using Munchkin.Core.Model.Cards.Treasures.Permanent;
using Munchkin.Core.Model.Exceptions;
using Munchkin.Core.Model.Expansions;
using Munchkin.Primitives.Abstractions;
using Munchkin.Runtime.Services;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static Munchkin.Core.Model.Cards.MunchkinDeluxeCards;

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
            var joined1 = await tableService.JoinTableAsync(table.GetUniqueId(), PlayerJohnyCashNickname);
            var joined2 = await tableService.JoinTableAsync(table.GetUniqueId(), PlayerFrankSinatraNickname);
            var joined3 = await tableService.JoinTableAsync(table.GetUniqueId(), PlayerElonMuskNickname);

            joined1.Should().NotBeNull();
            joined1.Should().BeEquivalentTo(JoinTableResult.Joined);
            joined2.Should().NotBeNull();
            joined2.Should().BeEquivalentTo(JoinTableResult.Joined);
            joined3.Should().NotBeNull();
            joined3.Should().BeEquivalentTo(JoinTableResult.Joined);

            table = await tableService.SetupAsync(table.GetUniqueId());

            table.Should().NotBeNull();
            table.Players.Count.Should().Be(3);

            // NOTE: try to join after the game has started
            var joined4 = await tableService.JoinTableAsync(table.GetUniqueId(), PlayerPeterJacksonNickname);

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
            table = await dungeonService.KickOpenTheDoorAsync(table.GetUniqueId());

            table.Players.Current.YourHand.Should().ContainSingle(x => x.Code == Doors.DwarfRace1);
            table.Players.Current.YourHand.Should().HaveCount(9);

            // PHASE: Loot The Room / Look For Trouble
            table = await dungeonService.LootTheRoomAsync(table.GetUniqueId());

            table.Players.Current.YourHand.Should().ContainSingle(x => x.Code == Doors.WanderingMonster1);
            table.Players.Current.YourHand.Should().HaveCount(10);

            // CHECK: Try to change turn when the player has more than 5 cards in hand
            var nextTurnActionException = await Record.ExceptionAsync(() => tableService.NextAsync(table.GetUniqueId()));

            nextTurnActionException.Should().NotBeNull();
            nextTurnActionException.Should().BeOfType<PlayerHasTooManyCardsInHandException>();

            // PHASE: Charity
            table = await tableService.EquipAsync(table.GetUniqueId(), PlayerJohnyCashNickname, Doors.ClericClass1);
            table = await tableService.EquipAsync(table.GetUniqueId(), PlayerJohnyCashNickname, Treasures.SneakyBastardSword);
            table = await tableService.DiscardAsync(table.GetUniqueId(), Doors.GelatinousOctahedron);
            table = await tableService.DiscardAsync(table.GetUniqueId(), Doors.Ancient);
            table = await tableService.DiscardAsync(table.GetUniqueId(), Treasures.ElevenFootPole);

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

            table = await tableService.NextAsync(table.GetUniqueId());

            table.Should().NotBeNull();
            table.Players.Current.Should().NotBeNull();
            table.Players.Current.Nickname.Should().BeEquivalentTo(PlayerFrankSinatraNickname);

            // PHASE: Play Cards
            table = await tableService.PlayAsync(table.GetUniqueId(), PlayerFrankSinatraNickname, Treasures.ThousandGoldPieces);
            table = await tableService.PlayAsync(table.GetUniqueId(), PlayerFrankSinatraNickname, Treasures.MutilateTheBodies);

            table.Players.Current.YourHand.Should().HaveCount(6);
            table.DungeonCards.Should().HaveCount(2);

            // PHASE: Kick Open The Door
            table = await dungeonService.KickOpenTheDoorAsync(table.GetUniqueId());

            table.Players.Current.YourHand.Should().ContainSingle(x => x.Code == Doors.WarriorClass1);
            table.Players.Current.YourHand.Should().HaveCount(7);

            // PHASE: Loot The Room / Look For Trouble
            var playerDoesNotOwnTheCardException = await Record.ExceptionAsync(() => dungeonService.LookForTroubleAsync(table.GetUniqueId(), Doors.Platycore));
            
            playerDoesNotOwnTheCardException.Should().NotBeNull();
            playerDoesNotOwnTheCardException.Should().BeOfType<PlayerDoesNotOwnTheCardException>();
            
            table = await dungeonService.LookForTroubleAsync(table.GetUniqueId(), Doors.PottedPlant);

            table.Players.Current.YourHand.Should().HaveCount(6);
            table.DungeonCards.Should().ContainSingle(x => x.Code == Doors.PottedPlant);
            table.DungeonCards.Should().HaveCount(3);

            table = await combatService.RewardAsync(table.GetUniqueId());

            table.Players.Current.YourHand.Should().ContainSingle(x => x.Code == Treasures.BoilAnAnthill);
            table.Players.Current.Level.Should().Be(4);
            table.Players.Current.YourHand.Should().HaveCount(7);
            table.DiscardedDoorsCards.Should().HaveCount(2);
            table.DungeonCards.Should().HaveCount(3);

            // PHASE: Charity
            table = await charityService.GiveAwayAsync(table.GetUniqueId(), Treasures.BoilAnAnthill, PlayerFrankSinatraNickname, PlayerElonMuskNickname);
            table = await charityService.GiveAwayAsync(table.GetUniqueId(), Treasures.WishingRing1, PlayerFrankSinatraNickname, PlayerElonMuskNickname);

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

            table = await tableService.NextAsync(table.GetUniqueId());

            table.Should().NotBeNull();
            table.Players.Current.Should().NotBeNull();
            table.Players.Current.Nickname.Should().BeEquivalentTo(PlayerElonMuskNickname);

            // PHASE: Play Cards
            table = await tableService.EquipAsync(table.GetUniqueId(), PlayerElonMuskNickname, Doors.ThiefClass1);
            table = await tableService.EquipAsync(table.GetUniqueId(), PlayerElonMuskNickname, Treasures.CloakOfObscurity);
            table = await tableService.EquipAsync(table.GetUniqueId(), PlayerElonMuskNickname, Treasures.DaggerOfTreachery);

            // PHASE: Kick Open The Door
            table = await dungeonService.KickOpenTheDoorAsync(table.GetUniqueId());

            table.DungeonCards.Should().ContainSingle(x => x.Code == Doors.ChangeClass);
            table.DungeonCards.Should().HaveCount(1);

            var cannotCancelCurseExeption = await Record.ExceptionAsync(() => curseService.ResolveAsync(table.GetUniqueId(), Doors.ChangeClass, Treasures.CloakOfObscurity));

            cannotCancelCurseExeption.Should().NotBeNull();
            cannotCancelCurseExeption.Should().BeOfType<CurseCannotBeCancelledWithTheChosenCardException>();

            table = await curseService.ResolveAsync(table.GetUniqueId(), Doors.ChangeClass, Treasures.WishingRing1);
            table = await tableService.CursePlayerAsync(table.GetUniqueId(), PlayerElonMuskNickname, Doors.ChickenOnYourHead);
            table = await curseService.TakeBadStuffAsync(table.GetUniqueId(), Doors.ChickenOnYourHead);

            table.Players.Current.Level.Should().Be(1);
            table.Players.Current.YourHand.Should().HaveCount(5);
            table.DiscardedDoorsCards.Should().HaveCount(3);
            table.DungeonCards.Should().HaveCount(1);

            // PHASE: Loot The Room / Look For Trouble
            table = await dungeonService.LookForTroubleAsync(table.GetUniqueId(), Doors.PlutoniumDragon);

            // PHASE: Charity
            table = await charityService.GiveAwayAsync(table.GetUniqueId(), Treasures.BoilAnAnthill, PlayerFrankSinatraNickname, PlayerElonMuskNickname);
            table = await charityService.GiveAwayAsync(table.GetUniqueId(), Treasures.WishingRing1, PlayerFrankSinatraNickname, PlayerElonMuskNickname);

            table.Players.Current.YourHand.Should().HaveCount(5);
            table.Players.First(x => x.Nickname == PlayerElonMuskNickname).Backpack.Should().HaveCount(2);

            return table;
        }

        private class NonShuffleAlgorithm<T> : IShuffleAlgorithm<T>
        {
            public void Shuffle(T[] array) { }
        }
    }

    public static class TableExtensions
    {
        public static string GetUniqueId(this Table table) => TableService.GetUniqueId(table);
    }
}

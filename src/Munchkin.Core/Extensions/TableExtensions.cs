using MediatR;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Requests;
using Munchkin.Core.Model.Stages;
using Munchkin.Extensions.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Extensions
{
    public static class TableExtensions
    {
        public static async Task<Table> SetupAsync(IMediator mediator, IReadOnlyCollection<IExpansion> expansions)
        {
            var table = Table.Empty();

            // NOTE: set required level to win,
            table = await table.Unit()
                .SelectMany(x => x.WithRequestSink(mediator))
                .SelectMany(x => x.WithWinningLevel(10));

            // NOTE: shuffle in all the selected expansions
            table = await expansions
                .Aggregate(table.Unit(), (table, expansion) => table
                    .SelectMany(x => x.WithTreasureDeck(expansion.TreasureDeck.GetTreasureCards()))
                    .SelectMany(x => x.WithDoorDeck(expansion.DoorDeck.GetDoorsCards())));

            // NOTE: shuffle the decks for randomness
            table.DoorsCardDeck.Shuffle();
            table.TreasureCardDeck.Shuffle();

            // NOTE: give all players initial cards
            table.Players.ForEach(player => PlayerAvatar.Revive(table, player));

            return table;
        }

        /// <summary>
        /// Gets if any of the players has won the game.
        /// </summary>
        public static bool IsGameOver(this Table table) => table.Players.Any(x => x.Level >= table.WinningLevel);

        /// <summary>
        /// Gets if the table is empty and has no players besides it.
        /// </summary>
        /// <param name="table">The table to chak against.</param>
        public static bool IsEmpty(this Table table) => !table.Players.Any();

        public static async Task<Table> WaitForAllPlayersAsync(this Table table)
        {
            // NOTE: map each player to their own Task Completion Source, so that they can end combat
            var playerResponses = table.Players
                .Select(player => new GameWaitForPlayerRequest(table, player))
                .Select(request => table.RequestSink.Send(request));

            // NOTE: select and wait for all players to end combat
            await Task.WhenAll(playerResponses);

            return table;
        }
    }
}

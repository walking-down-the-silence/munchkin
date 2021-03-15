using MediatR;
using Munchkin.Core.Contracts;
using Munchkin.Core.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class SetupTableStep : ITerminalStep<Table>
    {
        public async Task<Table> Resolve(Table table)
        {
            // TODO: pass a valid reference for Mediator
            return Setup(table, null, null, null, null);
        }

        /// <summary>
        /// Begins the game
        /// </summary>
        /// <param name="winningLevel"> The winning level. </param>
        private static Table Setup(
            Table table,
            IMediator mediator,
            IEnumerable<Player> players,
            ITreasuresFactory treasuresFactory,
            IDoorsFactory doorsFactory)
        {
            var playersList = new CircularList<Player>(players);
            var treasureCards = treasuresFactory.GetTreasureCards();
            var doorsCards = doorsFactory.GetDoorsCards();

            // TODO: initialize and shuffle the decks with all cards from factory
            // shuffle the decks for randomness
            table.DoorsCardDeck.Shuffle();
            table.TreasureCardDeck.Shuffle();

            // give all players initial cards
            table.Players.ForEach(player => player.Revive(table));

            return table;
        }
    }
}

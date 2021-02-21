using MediatR;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Cards;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core.Model
{
    /// <summary>
    /// Contains all data about the game & it's context
    /// </summary>
    public class Table
    {
        private Table(IMediator mediator, CircularList<Player> players, int winningLevel)
        {
            Players = players ?? throw new System.ArgumentNullException(nameof(players));
            RequestSink = mediator;
            WinningLevel = winningLevel;
            Dungeon = new Dungeon(this);

            // initialize and shuffle the decks
            TreasureCardDeck = new CardDeck<TreasureCard>();
            DoorsCardDeck = new CardDeck<DoorsCard>();
            DiscardedTreasureCards = new CardDeck<TreasureCard>();
            DiscardedDoorsCards = new CardDeck<DoorsCard>();

            // give all players initial cards
            Players.ForEach(ReviveHero);
        }

        /// <summary>
        /// Begins the game
        /// </summary>
        /// <param name="winningLevel"> The winning level. </param>
        public static Table Setup(IMediator mediator, IEnumerable<Player> players, int winningLevel)
        {
            var playersList = new CircularList<Player>(players);
            return new Table(mediator, playersList, winningLevel);
        }

        /// <summary>
        /// Gets the list of players
        /// </summary>
        public CircularList<Player> Players { get; }

        /// <summary>
        /// Gets the treasures card deck
        /// </summary>
        public CardDeck<TreasureCard> TreasureCardDeck { get; }

        /// <summary>
        /// Gets the doors card deck
        /// </summary>
        public CardDeck<DoorsCard> DoorsCardDeck { get; }

        /// <summary>
        /// Gets the discarded treasure cards
        /// </summary>
        public CardDeck<TreasureCard> DiscardedTreasureCards { get; }

        /// <summary>
        /// Gets the discarded door cards
        /// </summary>
        public CardDeck<DoorsCard> DiscardedDoorsCards { get; }

        /// <summary>
        /// Dungeon state representing all goods in this dungeon
        /// </summary>
        public Dungeon Dungeon { get; private set; }

        /// <summary>
        /// Request sink that is used for player interaction when a selection or decision is needed
        /// </summary>
        public IMediator RequestSink { get; }

        /// <summary>
        /// The winning level number
        /// </summary>
        public int WinningLevel { get; }

        public bool IsGameWon => Players.Any(x => x.Level >= WinningLevel);

        public void PlayFromHand(Player player, Card card)
        {
            //InPlay.Add(card);
            //player.YourHand.Remove(card);
        }

        public void PlayFromTable(Player player, Card card)
        {
            //InPlay.Add(card);
            //player.YourHand.Remove(card);
        }

        public void EquipFromHand(Player player, Card card)
        {
            //player.Equipped.Add(card);
            //player.YourHand.Remove(card);
        }

        public void EquipFromTable(Player player, Card card)
        {
            //player.Equipped.Add(card);
            //player.Backpack.Remove(card);
        }

        /// <summary>
        /// Revives players hero and give intital cards
        /// </summary>
        /// <param name="player"> Player to revive. </param>
        private void ReviveHero(Player player)
        {
            var cards = Enumerable.Empty<Card>()
                .Concat(DoorsCardDeck.TakeRange(4))
                .Concat(TreasureCardDeck.TakeRange(4));
            player.Revive(cards);
        }
    }
}

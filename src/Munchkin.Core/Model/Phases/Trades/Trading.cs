using Munchkin.Core.Contracts.Cards;
using System.Collections.Immutable;

namespace Munchkin.Core.Model.Phases.Trades
{
    /// <summary>
    /// Defines a set of operations available during the trade.
    /// </summary>
    public static class Trading
    {
        /// <summary>
        /// Creates the initial state for the trade.
        /// </summary>
        /// <param name="left">The left party of the offer.</param>
        /// <param name="right">The right party of the offer.</param>
        /// <returns>An instance of the trade state object.</returns>
        public static Trade InitiateTrade(Player left, Player right)
        {
            var emptyCollection = ImmutableList<Card>.Empty;
            var leftSide = new TradingSide(left, emptyCollection, PlayerTradeChoice.None);
            var rightSide = new TradingSide(right, emptyCollection, PlayerTradeChoice.None);
            return new Trade(leftSide, rightSide, TradingStatus.Ongoing);
        }

        /// <summary>
        /// Sets the left player decision in the offer and changes the trade state object.
        /// </summary>
        /// <param name="trade">The trade state object to change.</param>
        /// <param name="choice">The player's choice on the trade offer.</param>
        /// <returns>An instance of the trade state object with reflected changes.</returns>
        public static Trade SetLeftSideDecisionTrade(this Trade trade, PlayerTradeChoice choice)
        {
            return trade with
            {
                LeftSide = trade.LeftSide with { Decision = choice },
                Status = GetTradingStatus(choice, trade.RightSide.Decision)
            };
        }

        /// <summary>
        /// Sets the right player decision in the offer and changes the trade state object.
        /// </summary>
        /// <param name="trade">The trade state object to change.</param>
        /// <param name="choice">The player's choice on the trade offer.</param>
        /// <returns>An instance of the trade state object with reflected changes.</returns>
        public static Trade SetRightSideDecisionTrade(this Trade trade, PlayerTradeChoice choice)
        {
            return trade with
            {
                RightSide = trade.RightSide with { Decision = choice },
                Status = GetTradingStatus(trade.LeftSide.Decision, choice)
            };
        }

        /// <summary>
        /// Adds the collection of cards to the offer on behalf of the left party.
        /// </summary>
        /// <param name="cards">The collection of cards.</param>
        /// <returns>An instance of the trade state object with reflected changes.</returns>
        public static Trade AddToLeftSideOffer(this Trade trade, Card card)
        {
            var leftSide = trade.LeftSide with { OfferedCards = trade.LeftSide.OfferedCards.Add(card) };
            return trade with { LeftSide = leftSide };
        }

        /// <summary>
        /// Adds the collection of cards to the offer on behalf of the right party.
        /// </summary>
        /// <param name="cards">The collection of cards.</param>
        /// <returns>An instance of the trade state object with reflected changes.</returns>
        public static Trade AddToRightSideOffer(this Trade trade, Card card)
        {
            var rightSide = trade.RightSide with { OfferedCards = trade.RightSide.OfferedCards.Add(card) };
            return trade with { RightSide = rightSide };
        }

        /// <summary>
        /// Removes the collection of cards from the offer on behalf of the left party.
        /// </summary>
        /// <param name="cards">The collection of cards.</param>
        /// <returns>An instance of the trade state object with reflected changes.</returns>
        public static Trade RemoveFromLeftSideOffer(this Trade trade, Card card)
        {
            var leftSide = trade.LeftSide with { OfferedCards = trade.LeftSide.OfferedCards.Remove(card) };
            return trade with { LeftSide = leftSide };
        }

        /// <summary>
        /// Removes the collection of cards from the offer on behalf of the right party.
        /// </summary>
        /// <param name="cards">The collection of cards.</param>
        /// <returns>An instance of the trade state object with reflected changes.</returns>
        public static Trade RemoveFromRightSideOffer(this Trade trade, Card card)
        {
            var rightSide = trade.RightSide with { OfferedCards = trade.RightSide.OfferedCards.Remove(card) };
            return trade with { RightSide = rightSide };
        }

        private static TradingStatus GetTradingStatus(PlayerTradeChoice leftChoice, PlayerTradeChoice rightChoice)
        {
            return (leftChoice, rightChoice) switch
            {
                (PlayerTradeChoice.Confirm, PlayerTradeChoice.Confirm) => TradingStatus.Successfull,
                (PlayerTradeChoice.Reject, _) => TradingStatus.Rejected,
                (_, PlayerTradeChoice.Reject) => TradingStatus.Rejected,
                _ => TradingStatus.Ongoing
            };
        }
    }
}

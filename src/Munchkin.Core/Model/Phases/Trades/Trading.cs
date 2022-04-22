using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using System;
using System.Collections.Generic;
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
            var leftSide = new TradingSide(left, emptyCollection);
            var rightSide = new TradingSide(right, emptyCollection);
            return new Trade(leftSide, rightSide, TradingStatus.Ongoing, PlayerTradeChoice.None, PlayerTradeChoice.None);
        }

        /// <summary>
        /// Sets the left player decision in the offer and changes the trade state object.
        /// </summary>
        /// <param name="trade">The trade state object to change.</param>
        /// <param name="choice">The player's choice on the trade offer.</param>
        /// <returns>An instance of the trade state object with reflected changes.</returns>
        public static Trade SetLeftSideDecisionTrade(Trade trade, PlayerTradeChoice choice)
        {
            return trade with
            {
                LeftSideTradeDecision = choice,
                Status = GetTradingStatus(choice, trade.RightSideTradeDecision)
            };
        }

        /// <summary>
        /// Sets the right player decision in the offer and changes the trade state object.
        /// </summary>
        /// <param name="trade">The trade state object to change.</param>
        /// <param name="choice">The player's choice on the trade offer.</param>
        /// <returns>An instance of the trade state object with reflected changes.</returns>
        public static Trade SetRightSideDecisionTrade(Trade trade, PlayerTradeChoice choice)
        {
            return trade with
            {
                RightSideTradeDecision = choice,
                Status = GetTradingStatus(trade.LeftSideTradeDecision, choice)
            };
        }

        /// <summary>
        /// Adds the collection of cards to the offer on behalf of the left party.
        /// </summary>
        /// <param name="cards">The collection of cards.</param>
        /// <returns>An instance of the trade state object with reflected changes.</returns>
        public static Trade AddToLeftSideOffer(IReadOnlyCollection<Card> cards) => throw new NotImplementedException();

        /// <summary>
        /// Adds the collection of cards to the offer on behalf of the right party.
        /// </summary>
        /// <param name="cards">The collection of cards.</param>
        /// <returns>An instance of the trade state object with reflected changes.</returns>
        public static Trade AddToRightSideOffer(IReadOnlyCollection<Card> cards) => throw new NotImplementedException();

        /// <summary>
        /// Removes the collection of cards from the offer on behalf of the left party.
        /// </summary>
        /// <param name="cards">The collection of cards.</param>
        /// <returns>An instance of the trade state object with reflected changes.</returns>
        public static Trade RemoveFromLeftSideOffer(IReadOnlyCollection<Card> cards) => throw new NotImplementedException();

        /// <summary>
        /// Removes the collection of cards from the offer on behalf of the right party.
        /// </summary>
        /// <param name="cards">The collection of cards.</param>
        /// <returns>An instance of the trade state object with reflected changes.</returns>
        public static Trade RemoveFromRightSideOffer(IReadOnlyCollection<Card> cards) => throw new NotImplementedException();

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

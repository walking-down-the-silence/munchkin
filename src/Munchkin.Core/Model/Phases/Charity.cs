using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Exceptions;
using Munchkin.Core.Model.Phases.Events;
using System;

namespace Munchkin.Core.Model.Phases
{
    /// <summary>
    /// If you have more than five cards in your hand, you must play
    /// enough cards to get you to five or below.If you cannot, or do not want to, you
    /// must give the excess cards to the player with the lowest Level.If players are
    /// tied for lowest, divide the cards as evenly as possible, but it’s up to you who
    /// gets the bigger set(s) of leftovers.
    /// </summary>
    public static class Charity
    {
        /// <summary>
        /// Gives the chosen card from one player to another.
        /// </summary>
        /// <param name="table">The table where the game takes place.</param>
        /// <param name="giver">The player who gives away the card.</param>
        /// <param name="card">The card that is given</param>
        /// <param name="taker">The player who takes the card.</param>
        /// <returns>Returns an updated instance of the table.</returns>
        public static Table GiveAway(Table table, Player giver, Card card, Player taker)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(giver, nameof(giver));
            ArgumentNullException.ThrowIfNull(card, nameof(card));
            ArgumentNullException.ThrowIfNull(taker, nameof(taker));

            if (card.Owner?.Nickname != giver.Nickname)
                throw new PlayerDoesNotOwnTheCardException();

            giver.Discard(card);
            taker.PutInBackpack(card);

            var giveAwayEvent = new CharityGivenAwayEvent(
                giver.Nickname,
                taker.Nickname,
                card.GetHashCode().ToString());

            table.ActionLog.Add(giveAwayEvent);

            return table;
        }
    }
}

using Munchkin.Core.Contracts.Cards;

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
        public static Table GiveAway(Table table, Player giver, Card card, Player taker)
        {
            giver.Discard(card);
            taker.PutInBackpack(card);

            var giveAwayEvent = new CharityGivenAwayEvent(
                giver.Nickname,
                taker.Nickname,
                card.GetHashCode().ToString());

            table.ActionLog.Push(giveAwayEvent);

            return table;
        }
    }

    public record CharityGivenAwayEvent(
        string GiverNickname,
        string TakerNickname,
        string CardId);
}

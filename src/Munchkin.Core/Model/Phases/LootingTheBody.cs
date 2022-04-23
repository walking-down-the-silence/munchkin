using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Attributes;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using System.Collections.Immutable;
using System.Linq;

namespace Munchkin.Core.Model.Phases
{
    /// <summary>
    /// Defines the state when a player avatar has died and the cards needs to be salvaged.
    /// </summary>
    /// <param name="TakenFrom">The player whos avatar died.</param>
    /// <param name="TemporaryDiscard">The card collection that needs to be salvaged.</param>
    public record LootingTheBody(
        Table Table,
        Player TakenFrom,
        ImmutableList<Player> TakenBy,
        ImmutableList<Card> TemporaryDiscard
    ) : StateBase<LootingTheBody>(Table, TakenFrom, ImmutableList<Attribute>.Empty)
    {
        public static LootingTheBody From(Table table)
        {
            var targetPlayer = table.Players.Current;

            // NOTE: Starting with the player with the highest Level, everyone else chooses one
            // card... in case of ties in Level, roll a die.
            // Dead characters cannot receive cards for any reason, not even Charity, and
            // cannot level up or win the game.
            var otherPlayers = ImmutableList.CreateRange(table.Players
                .Where(p => p != targetPlayer)
                .Where(p => !p.IsDead)
                .OrderByDescending(p => p.Level));

            // NOTE: Looting The Body: Lay out your hand beside the cards you had in play
            // (making sure not to include the cards mentioned above). If you have an Item
            // carried by a Hireling or attached to a Cheat!card, separate those cards.
            var cardsToBeTaken = ImmutableList.CreateRange(targetPlayer.AllCards());

            // TODO: Ensure that player avatar is dead when reaching this point
            return new LootingTheBody(
                table,
                targetPlayer,
                otherPlayers,
                cardsToBeTaken);
        }
    }
}

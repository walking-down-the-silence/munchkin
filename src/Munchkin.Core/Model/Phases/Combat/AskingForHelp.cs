using Munchkin.Core.Contracts.Cards;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace Munchkin.Core.Model.Phases
{
    public record AskingForHelp(
        ImmutableList<Player> PlayersToAsk,
        Player AskedPlayer,
        ImmutableArray<Card> TemporaryPile)
    {
        public static AskingForHelp From(Table table)
        {
            var playersToAsk = ImmutableList.CreateRange(table.Players
                .Where(p => p != table.Players.Current)
                .Where(p => !p.IsDead));

            return new AskingForHelp(playersToAsk, null, ImmutableArray<Card>.Empty);
        }

        public AskingForHelp WithAskedPlayer(Player askedPlayer)
        {
            return this with
            {
                PlayersToAsk = PlayersToAsk.Remove(askedPlayer),
                AskedPlayer = askedPlayer
            };
        }

        public AskingForHelp WithoutAskedPlayer()
        {
            return this with
            {
                AskedPlayer = null
            };
        }
    }
}

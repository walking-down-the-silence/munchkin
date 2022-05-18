using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Phases.Events;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace Munchkin.Core.Model.Phases
{
    /// <summary>
    /// Defines the state when a fighting pllayer asks another player for help in combat.
    /// </summary>
    /// <param name="PlayersLeftToAsk">A collection of remaining players to ask.</param>
    /// <param name="PlayerAsked">A player recently asked to help.</param>
    public record AskingForHelp(
        Player FightingPlayer,
        Player HelpingPlayer,
        ImmutableArray<Player> PlayersLeftToAsk,
        ImmutableArray<Player> PlayersWhoRejected,
        Player PlayerAsked)
    {
        public static AskingForHelp From(Table table)
        {
            // TODO: decide if this object requires the player recently asked or simply all players left to ask
            // TODO: filter by player selected above
            var playersLeftToAsk = ImmutableArray.CreateRange(table.Players
                .Where(player => player != table.Players.Current)
                .Where(player => !player.IsDead()));

            var askingForHelp = new AskingForHelp(
                table.Players.Current,
                default,
                playersLeftToAsk,
                ImmutableArray<Player>.Empty,
                default);

            askingForHelp = table.ActionLog
                .OfType<IAskingForHelpEvent>()
                .OrderBy(x => x.CreatedDate)
                .Aggregate(askingForHelp, (result, @event) => Apply(table, result, @event));

            return askingForHelp;
        }

        private static AskingForHelp Apply(Table table, AskingForHelp help, IAskingForHelpEvent helpEvent)
        {
            return helpEvent switch
            {
                AskingForHelpPlayerEvent event1 => Apply(table, help, event1),
                AskingForHelpAcceptedEvent event2 => Apply(table, help, event2),
                AskingForHelpRejectedEvent event3 => Apply(table, help, event3),
                _ => throw new ArgumentOutOfRangeException(nameof(helpEvent))
            };
        }

        private static AskingForHelp Apply(Table table, AskingForHelp help, AskingForHelpPlayerEvent helpEvent)
        {
            var player = table.Players.FirstOrDefault(x => x.Nickname == helpEvent.AskedPlayerNickname);
            return help.WithAskedPlayer(player);
        }

        private static AskingForHelp Apply(Table table, AskingForHelp help, AskingForHelpAcceptedEvent helpEvent)
        {
            return help.WithAcceptedPlayer();
        }

        private static AskingForHelp Apply(Table table, AskingForHelp help, AskingForHelpRejectedEvent helpEvent)
        {
            return help.WithRejectedPlayer();
        }

        /// <summary>
        /// Sets the player who was recently asked to help.
        /// </summary>
        /// <param name="askedPlayer">The player asked.</param>
        /// <returns>Retuns a modified instance of the state.</returns>
        public AskingForHelp WithAskedPlayer(Player askedPlayer)
        {
            return this with
            {
                PlayersLeftToAsk = PlayersLeftToAsk.Remove(askedPlayer),
                PlayerAsked = askedPlayer
            };
        }

        /// <summary>
        /// Sets the player as the one who accepted help request.
        /// </summary>
        /// <returns>Retuns a modified instance of the state.</returns>
        public AskingForHelp WithAcceptedPlayer()
        {
            return this with
            {
                HelpingPlayer = PlayerAsked,
                PlayerAsked = null
            };
        }

        /// <summary>
        /// Sets asked player to noone.
        /// </summary>
        /// <returns>Retuns a modified instance of the state.</returns>
        public AskingForHelp WithRejectedPlayer()
        {
            return this with
            {
                PlayersWhoRejected = PlayersWhoRejected.Add(PlayerAsked),
                PlayerAsked = null
            };
        }
    }
}

using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Attributes;
using Munchkin.Core.Model.Exceptions;
using Munchkin.Core.Model.Phases.Events;
using System;

namespace Munchkin.Core.Model.Phases
{
    /// <summary>
    /// Defines a set of actions available when the player is cursed.
    /// </summary>
    public static class Cursing
    {
        /// <summary>
        /// Resolves the curse with a chosen card.
        /// </summary>
        /// <param name="table">The table where the game takes place.</param>
        /// <param name="card">The card that should resolve the curse.</param>
        /// <returns>Returns an updated instance of the table.</returns>
        /// <exception cref="CurseCannotBeCancelledException">Thrown if the card cannot resolve curses.</exception>
        public static Table Resolve(Table table, CurseCard curse, Card card)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(curse, nameof(curse));
            ArgumentNullException.ThrowIfNull(card, nameof(card));

            if (card.Owner != table.Players.Current)
                throw new PlayerDoesNotOwnTheCardException();

            // NOTE: remove from player's cards and add it to the temporary pile,
            // before the step is resolved completely
            if (!card.HasAttribute<ResolveCurseAttribute>())
                throw new CurseCannotBeCancelledException();

            var curseResolvedEvent = new PlayerCurseResolvedEvent(table.Players.Current.Nickname, curse.Code, card.Code);
            table = table.WithActionEvent(curseResolvedEvent);
            table.Discard(card);

            return table;
        }

        /// <summary>
        /// Takes bad stuff from the curse and applies it to the player.
        /// </summary>
        /// <param name="table">The table where the game takes place.</param>
        /// <param name="curse">The curse to take bad stuff from.</param>
        /// <returns>Returns an updated instance of the table.</returns>
        public static Table TakeBadStuff(Table table, CurseCard curse)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(curse, nameof(curse));

            curse.BadStuff(table);

            if (curse.OneShot)
                table.Discard(curse);

            var curseBadStuffEvent = new PlayerCurseBadStuffTakenEvent(table.Players.Current.Nickname, curse.Code);
            table = table.WithActionEvent(curseBadStuffEvent);

            return table;
        }
    }
}
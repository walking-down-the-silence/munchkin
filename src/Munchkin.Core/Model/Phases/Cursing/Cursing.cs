using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Attributes;
using Munchkin.Core.Model.Exceptions;

namespace Munchkin.Core.Model.Phases
{
    public static class Cursing
    {
        public static Table Resolve(Table table, Card card)
        {
            // NOTE: remove from player's cards and add it to the temporary pile,
            // before the step is resolved completely
            if (!card.HasAttribute<CancelCurseAttribute>())
                throw new InvalidCardUsedException("The card used does not have the attribute for cancelling curses.");

            table.Turns.Current.Player.Discard(card);
            table.TemporaryPile.Add(card);

            return table;
        }

        public static Table TakeBadStuff(Table table, CurseCard curse)
        {
            curse.BadStuff(table);

            return table;
        }
    }
}
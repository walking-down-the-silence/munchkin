using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Exceptions;
using Munchkin.Core.Model.Phases;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors
{
    public sealed class HelpMeOutHere : SpecialCard
    {
        public HelpMeOutHere() :
            base(MunchkinDeluxeCards.Doors.HelpMeOutHere, "Help Me Out Here")
        {
        }

        public Task Play(Table table, ItemCard card)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));

            var combat = Combat.From(table);
            var makesDiffernce = combat.IsLoosing() && combat.WillBeWinning(card.StrengthBonus);

            if (!makesDiffernce)
                throw new CardCannotBePlayedException("The card cannot be played, because conditions are not met.");

            return Task.CompletedTask;
        }
    }
}
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors
{
    public sealed class Mate : SpecialCard
    {
        public Mate() :
            base(MunchkinDeluxeCards.Doors.Mate, "Mate")
        {
        }

        public Task Play(Table table, MonsterCard monster)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(monster, nameof(monster));

            monster.Attributes.ForEach(AddAttribute);

            return Task.CompletedTask;
        }
    }
}
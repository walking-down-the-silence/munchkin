using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Phases;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors
{
    public sealed class OutToLunch : SpecialCard
    {
        public OutToLunch() :
            base(MunchkinDeluxeCards.Doors.OutToLunch, "Out To Lunch")
        {
        }

        public override Task Play(Table table)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));

            var combat = CombatStats.From(table);

            table = combat.Monsters.Aggregate(table, (table, monster) => table.Discard(monster));
            table = table.TakeTreasure(out var treasure1);
            table = table.TakeTreasure(out var treasure2);

            combat.FightingPlayer.TakeInHand(treasure1);
            combat.FightingPlayer.TakeInHand(treasure2);

            return Task.CompletedTask;
        }
    }
}
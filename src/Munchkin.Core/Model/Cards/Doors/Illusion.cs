using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Phases.Events;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors
{
    public sealed class Illusion : SpecialCard
    {
        public Illusion() :
            base(MunchkinDeluxeCards.Doors.Illusion, "Illusion")
        {
        }

        public Task Play(Table table, MonsterCard discardMonster, MonsterCard fromHandMonster)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(discardMonster, nameof(discardMonster));
            ArgumentNullException.ThrowIfNull(fromHandMonster, nameof(fromHandMonster));

            table = table.Discard(discardMonster);
            table = table.Play(fromHandMonster);

            var monsterRemoved = new CombatMosterRemovedEvent(table.Players.Current.Nickname, discardMonster.Code);
            table = table.WithActionEvent(monsterRemoved);

            var monsterAdded = new CombatMosterAddedEvent(table.Players.Current.Nickname, fromHandMonster.Code);
            table = table.WithActionEvent(monsterAdded);

            return Task.CompletedTask;
        }
    }
}
using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Actions;
using Munchkin.Core.Model.Attributes;
using Munchkin.Core.Model.Cards.Events;
using Munchkin.Core.Model.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Classes
{
    public sealed class WizardClass : ClassCard
    {
        public WizardClass() :
            base(MunchkinDeluxeCards.Doors.WizardClass1, "Wizard")
        {
            AddAttribute(new WizardAttribute());
        }

        public IAction<Table> CharmSpellAction { get; private set; }

        public IAction<Table> FlightSpellAction { get; private set; }

        public override Task Play(Table table)
        {
            CharmSpellAction = new WizardCharmSpellAction();

            return Task.CompletedTask;
        }

        public Table CharmSpell(Table table, MonsterCard monster)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(monster, nameof(monster));

            if (Owner.YourHand.Count < 3)
                throw new PlayerCannotPerformActionException("Player cannot use 'Charm Spell' ability and discard the hand, because there is not enough cards in hand (at least 3 required).");

            var rewardTreasuredBonusEvent = new CombatRewardTreasuresBonusEvent(monster.RewardTreasures);
            table.ActionLog.Add(rewardTreasuredBonusEvent);

            Owner.DiscardHand();
            table.Discard(monster);

            return table;
        }

        public Table FlightSpell(Table table, Card discardCard)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(discardCard, nameof(discardCard));

            if (Owner != discardCard.Owner)
                throw new PlayerDoesNotOwnTheCardException();

            if (table.ActionLog.OfType<CombatRunAwayBonusEvent>().Count() >= 3)
                throw new PlayerCannotPerformActionException("Player cannot use 'Flight Spell' ability, because it was used maximum times (3 per turn).");

            var runAwayBonus = new CombatRunAwayBonusEvent(Owner.Nickname, 1);
            table.ActionLog.Add(runAwayBonus);

            return table;
        }
    }
}
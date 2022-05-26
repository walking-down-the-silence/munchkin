using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Cards.Events;
using Munchkin.Core.Model.Exceptions;
using Munchkin.Extensions.Threading;
using System;
using System.Threading.Tasks;
using static Munchkin.Core.Model.Cards.MunchkinDeluxeCards;

namespace Munchkin.Core.Model.Actions
{
    public sealed class WizardCharmSpellAction : DynamicAction
    {
        public WizardCharmSpellAction(Player owner) :
            base(WizardClass.CharmSpell, "Charm Spell")
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
        }

        public Player Owner { get; }

        public MonsterCard Monster { get; set; }

        protected override bool OnCanExecute(Table table)
        {
            return Monster is not null
                && Owner.YourHand.Count >= 3;
        }

        protected override Task<Table> OnExecuteAsync(Table table)
        {
            return CharmSpell(table, Monster).Unit();
        }

        public Table CharmSpell(Table table, MonsterCard monster)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(monster, nameof(monster));

            if (Owner.YourHand.Count < 3)
                throw new PlayerCannotPerformActionException("Player cannot use 'Charm Spell' ability and discard the hand, because there is not enough cards in hand (at least 3 required).");

            var rewardTreasuredBonusEvent = new CombatRewardTreasuresBonusEvent(monster.RewardTreasures);
            table = table.WithActionEvent(rewardTreasuredBonusEvent);

            var charmSpellEvent = new WizardCharmSpellActionEvent(Owner.Nickname, monster.Code);
            table = table.WithActionEvent(charmSpellEvent);

            Owner.DiscardHand();
            table = table.Discard(monster);

            return table;
        }
    }
}
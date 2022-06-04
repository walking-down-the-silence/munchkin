using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Model.Effects;
using Munchkin.Core.Model.Restrictions;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class PottedPlant : MonsterCard
    {
        public PottedPlant() :
            base(MunchkinDeluxeCards.Doors.PottedPlant, "Potted Plant", 1, 1, 1, 0, false)
        {
            AddEffect(Effect
                .New(new TreasureRewardBonusEffect(1))
                .With(() => Rule
                    .New(new UsableByElfOnlyRestriction())));
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            return table;
        }
    }
}
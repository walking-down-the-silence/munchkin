using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Effects;
using Munchkin.Core.Model.Restrictions;
using System;
using System.Linq;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class Bigfoot : MonsterCard
    {
        public Bigfoot() :
            base(MunchkinDeluxeCards.Doors.Bigfoot, "Bigfoot", 12, 1, 3, 0, false)
        {
            AddEffect(Effect
                .New(new MonsterStrengthBonusEffect(3))
                .With(() => Rule
                    .New(new UsableByDwarfOnlyRestriction()
                    .Or(new UsableByHalflingOnlyRestriction()))));
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            player.Equipped
                .OfType<WearingCard>()
                .Where(x => x.WearingType == EWearingType.Headgear)
                .ForEach(x => x.Discard(table));

            return table;
        }
    }
}
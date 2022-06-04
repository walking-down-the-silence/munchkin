using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Effects;
using Munchkin.Core.Model.Restrictions;
using System;
using System.Linq;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class DroolingSlime : MonsterCard
    {
        public DroolingSlime() :
            base(MunchkinDeluxeCards.Doors.DroolingSlime, "Drooling Slime", 1, 1, 1, 0, false)
        {
            AddEffect(Effect
                .New(new MonsterStrengthBonusEffect(4))
                .With(() => Rule
                    .New(new UsableByElfOnlyRestriction())));
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            var equippedFootgears = player.Equipped.OfType<WearingCard>()
                .Where(x => x.WearingType == EWearingType.Footgear);

            if (equippedFootgears.Any())
            {
                equippedFootgears.ForEach(card => card.Discard(table));
            }
            else
            {
                player.LevelDown();
            }

            return table;
        }
    }
}
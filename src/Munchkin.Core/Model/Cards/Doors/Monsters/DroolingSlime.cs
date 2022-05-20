using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Effects;
using Munchkin.Core.Model.Restrictions;
using System.Linq;
using System.Threading.Tasks;

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

        public override Task BadStuff(Table state)
        {
            var equippedFootgears = state.Players.Current.Equipped.OfType<WearingCard>()
                .Where(x => x.WearingType == EWearingType.Footgear);

            if (equippedFootgears.Any())
            {
                equippedFootgears.ForEach(card => card.Discard(state));
            }
            else
            {
                state.Players.Current.LevelDown();
            }

            return Task.CompletedTask;
        }
    }
}
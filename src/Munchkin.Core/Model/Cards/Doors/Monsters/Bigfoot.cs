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

        public override Task BadStuff(Table state)
        {
            state.Players.Current.Equipped
                .OfType<WearingCard>()
                .Where(x => x.WearingType == EWearingType.Headgear)
                .ForEach(x => x.Discard(state));

            return Task.CompletedTask;
        }
    }
}
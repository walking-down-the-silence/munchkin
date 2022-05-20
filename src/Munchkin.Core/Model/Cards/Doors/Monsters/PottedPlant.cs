using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Model.Effects;
using Munchkin.Core.Model.Restrictions;
using System.Threading.Tasks;

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

        public override Task BadStuff(Table state) => Task.CompletedTask;
    }
}
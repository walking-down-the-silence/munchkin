using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Model.Effects;
using Munchkin.Core.Model.Restrictions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class Harpies : MonsterCard
    {
        public Harpies() :
            base(MunchkinDeluxeCards.Doors.Harpies, "Harpies", 4, 1, 2, 0, false)
        {
            AddEffect(Effect
              .New(new MonsterStrengthBonusEffect(5))
              .With(() => Rule
                  .New(new UsableByWizardOnlyRestriction())));
        }

        public override Task BadStuff(Table state)
        {
            state.Players.Current.LevelDown();
            state.Players.Current.LevelDown();

            return Task.CompletedTask;
        }
    }
}
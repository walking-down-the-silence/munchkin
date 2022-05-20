using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Model.Effects;
using Munchkin.Core.Model.Restrictions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class MaulRat : MonsterCard
    {
        public MaulRat() : 
            base(MunchkinDeluxeCards.Doors.MaulRat, "Maul Rat", 1, 1, 1, 0, false)
        {
            AddEffect(Effect
                .New(new MonsterStrengthBonusEffect(3))
                .With(() => Rule
                    .New(new UsableByClericOnlyRestriction())));
        }

        public override Task BadStuff(Table state)
        {
            state.Players.Current.LevelDown();
            return Task.CompletedTask;
        }
    }
}
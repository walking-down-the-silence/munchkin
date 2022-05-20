using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Effects;
using Munchkin.Core.Model.Restrictions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class BandOf3872Orcs : MonsterCard
    {
        public BandOf3872Orcs() : 
            base(MunchkinDeluxeCards.Doors.BandOf3872Orcs, "3,872 Orcs", 10, 1, 3, 0, false)
        {
            AddEffect(Effect
                .New(new MonsterStrengthBonusEffect(6))
                .With(() => Rule
                    .New(new UsableByDwarfOnlyRestriction())));
        }

        public override async Task BadStuff(Table state)
        {
            var diceRollResult = Dice.Roll();

            if (diceRollResult <= 2)
            {
                state.KillPlayer(state.Players.Current);
            }
            else
            {
                for (int i = 0; i < diceRollResult; i++)
                {
                    state.Players.Current.LevelDown();
                }
            }
        }
    }
}

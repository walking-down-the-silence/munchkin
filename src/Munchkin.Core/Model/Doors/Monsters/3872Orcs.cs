using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Effects;
using Munchkin.Core.Model.Rules;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class BandOf3872Orcs : MonsterCard
    {
        public BandOf3872Orcs() : base("3,872 Orcs", 10, 1, 3, 0, false)
        {
            AddEffect(Effect
                .New(new MonsterStrengthBonusEffect(6))
                .With(() => Rule
                    .New(new HasDwarfRaceRule())));
        }

        public override async Task BadStuff(Table state)
        {
            var diceRollResult = Dice.Roll();

            if (diceRollResult <= 2)
            {
                await state.Players.Current.Kill(state);
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

using Munchkin.Core.Cards.Effects;
using Munchkin.Core.Cards.Rules;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Requests;
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

        public override async Task BadStuff(Table context)
        {
            var diceRollRequest = new DiceRollRequest(context.Players.Current);
            var diceRollResult = await context.Mediator.Send(diceRollRequest);
            if (diceRollResult <= 2)
            {
                context.Players.Current.Kill();
            }
            else
            {
                for (int i = 0; i < diceRollResult; i++)
                {
                    context.Players.Current.LevelDown();
                }
            }
        }
    }
}

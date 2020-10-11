using Munchkin.Core.Cards.Effects;
using Munchkin.Core.Cards.Rules;
using Munchkin.Core.Contracts;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class MaulRat : MonsterCard
    {
        public MaulRat() : base("Maul Rat", 1, 1, 1, 0, false)
        {
            AddEffect(Effect
                .New(new MonsterStrengthBonusEffect(3))
                .With(() => Rule
                    .New(new HasClericClassRule())));
        }

        public override Task Play(Table context)
        {
            // apply dynamic effects if conditions are met
            Effects.Where(effect => effect.Satisfies(context)).ForEach(effect => effect.Apply(context));

            // apply static effects
            return base.Play(context);
        }

        public override Task BadStuff(Table context)
        {
            // apply the bad stuff effects
            context.Players.Current.LevelDown();
            return Task.CompletedTask;
        }
    }
}
using Munchkin.Core.Cards.Effects;
using Munchkin.Core.Cards.Rules;
using Munchkin.Core.Contracts;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Requests;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class Platycore : MonsterCard
    {
        public Platycore() : base("Platycore", 6, 1, 2, 0, false)
        {
            AddEffect(Effect
                .New(new MonsterStrengthBonusEffect(6))
                .With(() => Rule
                    .New(new HasWizardClassRule())));
        }

        public override Task Play(Table context)
        {
            // apply dynamic effects if conditions are met
            Effects.Where(effect => effect.Satisfies(context)).ForEach(effect => effect.Apply(context));

            return base.Play(context);
        }

        public async override Task BadStuff(Table context)
        {
            var request = new DiscardHandOrLoose2LevelsRequest(context.Players.Current, context);
            var response = await context.RequestSink.Send(request);
            var action = await response.Task;

            if (action == DiscardHandOrLoose2LevelsActions.DiscardHand)
            {
                context.Players.Current.DiscardHand();
            }
            else
            {
                context.Players.Current.LevelDown();
                context.Players.Current.LevelDown();
            }
        }
    }
}
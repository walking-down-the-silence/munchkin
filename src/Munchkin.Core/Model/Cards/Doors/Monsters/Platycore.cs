using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Effects;
using Munchkin.Core.Model.Requests;
using Munchkin.Core.Model.Requests.Enums;
using Munchkin.Core.Model.Rules;
using Munchkin.Core.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
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

        public async override Task BadStuff(Table state)
        {
            var request = new PlayerDiscardHandOrLoose2LevelsRequest(state.Players.Current, state);
            var response = await state.RequestSink.Send(request);
            var action = await response.Task;

            if (action == DiscardHandOrLoose2LevelsActions.DiscardHand)
            {
                PlayerAvatar.DiscardHand(state, state.Players.Current);
            }
            else
            {
                state.Players.Current.LevelDown();
                state.Players.Current.LevelDown();
            }
        }
    }
}
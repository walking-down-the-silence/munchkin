using System.Linq;
using System.Threading.Tasks;
using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Effects;
using Munchkin.Core.Model.Enums;
using Munchkin.Core.Model.Rules;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class FaceSucker : MonsterCard
    {
        public FaceSucker() : base("Face Sucker", 8, 1, 2, 0, false)
        {
            AddEffect(Effect
               .New(new MonsterStrengthBonusEffect(6))
               .With(() => Rule
                 .New(new HasElfRaceRule())));
        }

        public override Task BadStuff(Table state)
        {
            state.Players.Current.Equipped
                .OfType<PermanentItemCard>()
                .Where(x => x.WearingType == EWearingType.Headgear)
                .ForEach(x => x.Discard(state));

            state.Players.Current.LevelDown();

            return Task.CompletedTask;
        }
    }
}
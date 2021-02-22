using Munchkin.Core.Cards.Effects;
using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using Munchkin.Core.Model.Rules;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class Bigfoot : MonsterCard
    {
        public Bigfoot() : base("Bigfoot", 12, 1, 3, 0, false)
        {
            AddEffect(Effect
                .New(new MonsterStrengthBonusEffect(3))
                .With(() => Rule
                    .New(new HasDwarfRaceRule()
                    .Or(new HasHalflingRaceRule()))));
        }

        public override Task BadStuff(Table state)
        {
            state.Players.Current.Equipped
                .OfType<PermanentItemCard>()
                .Where(x => x.WearingType == EWearingType.Headgear)
                .ForEach(x => x.Discard(state));

            return Task.CompletedTask;
        }
    }
}
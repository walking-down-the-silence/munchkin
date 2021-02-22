using Munchkin.Core.Cards.Effects;
using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using Munchkin.Core.Model.Rules;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class DroolingSlime : MonsterCard
    {
        public DroolingSlime() : base("Drooling Slime", 1, 1, 1, 0, false)
        {
            AddEffect(Effect
                .New(new MonsterStrengthBonusEffect(4))
                .With(() => Rule
                    .New(new HasElfRaceRule())));
        }

        public override Task BadStuff(Table state)
        {
            var equippedFootgears = state.Players.Current.Equipped.OfType<PermanentItemCard>()
                .Where(x => x.WearingType == EWearingType.Footgear);

            if (equippedFootgears.Any())
            {
                foreach (var equippedFootgear in equippedFootgears)
                {
                    state.Players.Current.Discard(equippedFootgear);
                }
            }
            else
            {
                state.Players.Current.LevelDown();
            }

            return Task.CompletedTask;
        }
    }
}
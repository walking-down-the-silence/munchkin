using Munchkin.Core.Cards.Effects;
using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Rules;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class PottedPlant : MonsterCard
    {
        public PottedPlant() : base("Potted Plant", 1, 1, 1, 0, false)
        {
            AddEffect(Effect
                .New(new TreasureRewardBonusEffect(1))
                .With(() => Rule
                    .New(new HasElfRaceRule())));
        }

        public override Task BadStuff(Table state) => Task.CompletedTask;
    }
}
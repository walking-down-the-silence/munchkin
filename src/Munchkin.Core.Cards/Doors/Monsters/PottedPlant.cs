using System.Threading.Tasks;
using Munchkin.Core.Cards.Effects;
using Munchkin.Core.Cards.Rules;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class PottedPlant : MonsterCard
    {
        public PottedPlant() : base("PottedPlant", 1, 1, 1, 0, false)
        {
            AddEffect(Effect
                .New(new GiveExtraTreasureEffect(1))
                .With(() => Rule
                    .New(new HasElfRaceRule())));
        }

        public override Task BadStuff(Table state)
        {
            return Task.CompletedTask;
        }
    }
}
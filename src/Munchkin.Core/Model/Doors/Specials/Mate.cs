using System.Linq;
using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class Mate : SpecialCard
    {
        public Mate() : base("Mate")
        {
        }

        public override Task Play(Table context)
        {
            // take all strength properties bound to monster
            var oneShotProperties = BoundTo.BoundCards
                .NotOfType<Mate>()
                .SelectMany(x => x.Attributes)
                .ToList();

            // calculate mate monster strength and treasures
            int strength = oneShotProperties.OfType<StrengthBonusAttribute>().Select(x => x.Bonus).Aggregate((x, y) => x + y);
            int treasure = oneShotProperties.OfType<RewardTreasuresAttribute>().Select(x => x.Bonus).Aggregate((x, y) => x + y);

            // add mate monster strength
            //gameContext.Dungeon.State.PlayersStrength += strength;
            //gameContext.Dungeon.State.RewardTreasures += treasure;
            return Task.CompletedTask;
        }
    }
}
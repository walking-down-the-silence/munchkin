using System;
using System.Linq;
using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Properties;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class TongueDemon : MonsterCard
    {
        public TongueDemon() : base("Tongue Demon", 12, 1, 3, 0, false)
        {
        }

        public override Task Play(Table gameContext)
        {
            var currentHero = gameContext.Players.Current;
            var currentHeroIsCleric = currentHero.Equipped.OfType<ClericClass>().Any();

            var helpingHero = gameContext.Dungeon.Combat.HelpingPlayer;
            var helpingHeroIsCleric = helpingHero?.Equipped.OfType<ClericClass>().Any();

            if (currentHeroIsCleric || helpingHeroIsCleric != null && helpingHeroIsCleric.Value)
            {
                gameContext.Dungeon.Combat.AddProperty(new MonsterStrengthBonusAttribute(4));
            }

            return base.Play(gameContext);
        }

        public override Task BadStuff(Table gameContext)
        {
            throw new NotImplementedException();
        }
    }
}
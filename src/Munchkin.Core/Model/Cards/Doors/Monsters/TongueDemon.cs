using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Cards.Doors.Classes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class TongueDemon : MonsterCard
    {
        public TongueDemon() :
            base(MunchkinDeluxeCards.Doors.TongueDemon, "Tongue Demon", 12, 1, 3, 0, false)
        {
        }

        public override Task Play(Table gameContext)
        {
            var currentHero = gameContext.Players.Current;
            var currentHeroIsCleric = currentHero.Equipped.OfType<ClericClass>().Any();

            // TODO: check if current stage actually is a combat
            //var helpingHero = gameContext.Dungeon.Combat.HelpingPlayer;
            //var helpingHeroIsCleric = helpingHero?.Equipped.OfType<ClericClass>().Any();

            //if (currentHeroIsCleric || helpingHeroIsCleric != null && helpingHeroIsCleric.Value)
            //{
            //    gameContext.Dungeon.Combat.AddAttribute(new MonsterStrengthBonusAttribute(4));
            //}

            return base.Play(gameContext);
        }

        public override Task BadStuff(Table gameContext)
        {
            throw new NotImplementedException();
        }
    }
}
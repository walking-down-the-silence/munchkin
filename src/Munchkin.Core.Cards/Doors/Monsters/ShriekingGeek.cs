using System;
using System.Linq;
using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class ShriekingGeek : MonsterCard
    {
        public ShriekingGeek() : base("Shrieking Geek", 6, 1, 2, 0, false)
        {
        }

        public override Task Play(Table gameContext)
        {
            var currentHero = gameContext.Players.Current;
            var currentHeroIsWarrior = currentHero.Equipped.OfType<WarriorClass>().Any();

            var helpingHero = gameContext.Dungeon.Combat.HelpingPlayer;
            var helpingHeroIsWarrior = helpingHero?.Equipped.OfType<WarriorClass>().Any();

            return base.Play(gameContext);
        }

        public override Task BadStuff(Table gameContext)
        {
            throw new NotImplementedException();
        }
    }
}
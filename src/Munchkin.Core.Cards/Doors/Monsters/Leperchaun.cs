using System;
using System.Linq;
using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class Leperchaun : MonsterCard
    {
        public Leperchaun() : base("Leperchaun", 4, 1, 2, 0, false)
        {
        }

        public override Task Play(Table gameContext)
        {
            var currentHero = gameContext.Players.Current;
            var currentHeroIsElf = currentHero.Equipped.OfType<ElfRace>().Any();

            // TODO: check if current stage actually is a combat
            //var helpingHero = gameContext.Dungeon.Combat.HelpingPlayer;
            //var helpingHeroIsElf = helpingHero?.Equipped.OfType<ElfRace>().Any();

            return base.Play(gameContext);
        }

        public override Task BadStuff(Table gameContext)
        {
            throw new NotImplementedException();
        }
    }
}
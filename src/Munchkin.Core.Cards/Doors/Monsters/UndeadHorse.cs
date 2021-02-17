using System;
using System.Linq;
using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class UndeadHorse : MonsterCard
    {
        public UndeadHorse() : base("Undead Horse", 4, 1, 2, 0, true)
        {
        }

        public override Task Play(Table gameContext)
        {
            var currentHero = gameContext.Players.Current;
            var currentHeroIsDwarf = currentHero.Equipped.OfType<DwarfRace>().Any();

            // TODO: check if current stage actually is a combat
            //var helpingHero = gameContext.Dungeon.Combat.HelpingPlayer;
            //var helpingHeroIsDwarf = helpingHero?.Equipped.OfType<DwarfRace>().Any();

            return base.Play(gameContext);
        }

        public override Task BadStuff(Table gameContext)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class UnspeakablyAwfulIndescribableHorror : MonsterCard
    {
        public UnspeakablyAwfulIndescribableHorror() : base("Unspeakably Awful Indescribable Horror", 14, 1, 4, 0, false)
        {
        }

        public override Task Play(Table gameContext)
        {
            var currentHero = gameContext.Players.Current;
            var currentHeroIsWarrior = currentHero.Equipped.OfType<WarriorClass>().Any();

            // TODO: check if current stage actually is a combat
            //var helpingHero = gameContext.Dungeon.Combat.HelpingPlayer;
            //var helpingHeroIsWarrior = helpingHero?.Equipped.OfType<WarriorClass>().Any();

            return base.Play(gameContext);
        }

        public override Task BadStuff(Table gameContext)
        {
            throw new NotImplementedException();
        }
    }
}
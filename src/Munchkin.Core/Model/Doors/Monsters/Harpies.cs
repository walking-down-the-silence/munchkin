using System.Linq;
using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Properties;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class Harpies : MonsterCard
    {
        public Harpies() : base("Harpies", 4, 1, 2, 0, false)
        {
        }

        public override Task Play(Table gameContext)
        {
            var currentHero = gameContext.Players.Current;
            var currentHeroIsWizard = currentHero.Equipped.OfType<WizardClass>().Any();

            // TODO: check if current stage actually is a combat
            //var helpingHero = gameContext.Dungeon.Combat.HelpingPlayer;
            //var helpingHeroIsWizard = helpingHero?.Equipped.OfType<WizardClass>().Any();

            //if (currentHeroIsWizard || helpingHeroIsWizard != null && helpingHeroIsWizard.Value)
            //{
            //    gameContext.Dungeon.Combat.AddProperty(new PlayerStrengthBonusAttribute(5));
            //}

            return base.Play(gameContext);
        }

        public override Task BadStuff(Table gameContext)
        {
            gameContext.Players.Current.LevelDown();
            gameContext.Players.Current.LevelDown();
            return Task.CompletedTask;
        }
    }
}
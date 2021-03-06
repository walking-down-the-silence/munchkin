using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Engine.Original.Treasures;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core.Model
{
    public class MunchkinOriginalTreasuresFactory : ITreasureDeckFactory
    {
        public IReadOnlyCollection<TreasureCard> GetTreasureCards()
        {
            return CreateCardCollection().ToArray();
        }

        private IEnumerable<TreasureCard> CreateCardCollection()
        {
            // level ups
            yield return new LevelUpTreasure("1,000 Gold Pieces");
            yield return new LevelUpTreasure("Boil An Anthill");
            yield return new LevelUpTreasure("Bribe GM With Food");
            yield return new LevelUpTreasure("Convenient Addition Error");
            yield return new LevelUpTreasure("Whine At The GM");
            yield return new LevelUpTreasure("Mutilate The Bodies");
            yield return new LevelUpTreasure("Invoke Obscure Rules");
            yield return new LevelUpTreasure("Potion Of General Studliness");
            yield return new KillTheHireling();
            yield return new StealALevel();

            // one shot items
            yield return new FreezingExplosivePotion();
            yield return new FriendshipPotion();
            yield return new MagicLamp();
            yield return new PrettyBaloons();
            yield return new CotionOfPonfusion();
            yield return new Doppleganger();
            yield return new ElectricRadioactiveAcidPotion();
            yield return new FlamingPoisonPotion();
            yield return new FlaskOfGlue();
            yield return new Hoard();
            yield return new InstantWall();
            yield return new WishingRing();
            yield return new WishingRing();
            yield return new InvisibilityPotion();
            yield return new LoadedDie();
            yield return new MagicMissile();
            yield return new NastyTastingSportsDrink();
            yield return new PollymorphPotion();
            yield return new PotionOfHalitosis();
            yield return new PotionOfIdioticBravery();
            yield return new SleepPotion();
            yield return new TransferralPotion();
            yield return new WandOfDowsing();
            yield return new YuppieWater();
            yield return new QDice();

            // permanent items
            yield return new BootsOfRunningReallyFast();
            yield return new GentlemensClub();
            yield return new HugeRock();
            yield return new BadAssBandana();
            yield return new BootsOfButtKicking();
            yield return new BowWithRibbons();
            yield return new BroadSword();
            yield return new BucklerOfSwashing();
            yield return new ChainsawOfBloodyDismemberment();
            yield return new CheeseGreaterOfPeace();
            yield return new CloakOfObscurity();
            yield return new DaggerOfTreachery();
            yield return new ElevenFootPole();
            yield return new FlamingArmor();
            yield return new HammerOfKneecapping();
            yield return new HelmOfCourage();
            yield return new Hireling();
            yield return new HornyHelmet();
            yield return new KneepadsOfAllure();
            yield return new LeatherArmor();
            yield return new LimburgerAndAnchovySandwich();
            yield return new MaceOfSharpness();
            yield return new MithrilArmor();
            yield return new PantyhoseOfGiantStrength();
            yield return new PointyHatOfPower();
            yield return new RapierOfUnfairness();
            yield return new RatOnAStick();
            yield return new ReallyImpressiveTitle();
            yield return new SandalsOfProtection();
            yield return new ShieldOfUbiquity();
            yield return new ShortWideArmor();
            yield return new SingingAndDancingSword();
            yield return new SlimyArmor();
            yield return new SneakyBastardSword();
            yield return new SpikyKnees();
            yield return new StaffOfNapalm();
            yield return new Stepladder();
            yield return new SwissArmyPolearm();
            yield return new TubaOfCharm();
        }
    }
}
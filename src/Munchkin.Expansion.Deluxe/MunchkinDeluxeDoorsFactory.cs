using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Cards.Doors;
using Munchkin.Core.Model.Cards.Doors.Classes;
using Munchkin.Core.Model.Cards.Doors.Curses;
using Munchkin.Core.Model.Cards.Doors.Enhancers;
using Munchkin.Core.Model.Cards.Doors.Monsters;
using Munchkin.Core.Model.Cards.Doors.Races;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Expansion.Deluxe
{
    public class MunchkinDeluxeDoorsFactory : IDoorDeckFactory
    {
        public IReadOnlyCollection<DoorsCard> GetDoorsCards()
        {
            return CreateCardCollection().ToArray();
        }

        private static IEnumerable<DoorsCard> CreateCardCollection()
        {
            yield return new Halfbreed();
            yield return new Halfbreed();
            yield return new SuperMunchkin();
            yield return new SuperMunchkin();

            // monsters
            yield return new Crabs();
            yield return new DroolingSlime();
            yield return new FlyingFrogs();
            yield return new GelatinousOctahedron();
            yield return new Harpies();
            yield return new FaceSucker();
            yield return new Ghoulfiends();
            yield return new Gazebo();
            yield return new Amazon();
            yield return new FloatingNose();
            yield return new BandOf3872Orcs();
            yield return new Bigfoot();
            yield return new Hippogriff();
            yield return new Bullrog();
            yield return new WightBrothers();
            yield return new WannabeVampire();
            yield return new UnspeakablyAwfulIndescribableHorror();
            yield return new UndeadHorse();
            yield return new TongueDemon();
            yield return new StonedGolem();
            yield return new Squidzilla();
            yield return new SnailsOnSpeed();
            yield return new ShriekingGeek();
            yield return new Pukachu();
            yield return new PottedPlant();
            yield return new PlutoniumDragon();
            yield return new Platycore();
            yield return new NetTroll();
            yield return new MisterBones();
            yield return new MaulRat();
            yield return new Leperchaun();
            yield return new Lawyers();
            yield return new LargeAngryChicken();
            yield return new LameGoblin();
            yield return new KingTut();
            yield return new InsuranceSalesman();

            // races
            yield return new DwarfRace();
            yield return new DwarfRace();
            yield return new DwarfRace();
            yield return new ElfRace();
            yield return new ElfRace();
            yield return new ElfRace();
            yield return new HalflingRace();
            yield return new HalflingRace();
            yield return new HalflingRace();

            // classes
            yield return new ClericClass();
            yield return new ClericClass();
            yield return new ClericClass();
            yield return new ThiefClass();
            yield return new ThiefClass();
            yield return new ThiefClass();
            yield return new WizardClass();
            yield return new WizardClass();
            yield return new WizardClass();
            yield return new WarriorClass();
            yield return new WarriorClass();
            yield return new WarriorClass();

            // specials
            yield return new WanderingMonster();
            yield return new WanderingMonster();
            yield return new WanderingMonster();
            yield return new Cheat();
            yield return new DivineIntervention();
            yield return new HelpMeOutHere();
            yield return new Illusion();
            yield return new OutToLunch();
            yield return new Mate();

            // enhancers
            yield return new Ancient();
            yield return new Baby();
            yield return new Enraged();
            yield return new Humongous();
            yield return new Intelligent();

            // curses
            yield return new ChangeSex();
            yield return new ChangeClass();
            yield return new ChangeRace();
            yield return new ChickenOnYourHead();
            yield return new DuckOfDoom();
            yield return new IncomeTax();
            yield return new LoseOneBigItem();
            yield return new LoseOneSmallItem();
            yield return new LoseOneSmallItem();
            yield return new LoseALevel();
            yield return new LoseALevel();
            yield return new LoseTheArmorYouAreWearing();
            yield return new LoseTheFootgearYouAreWearing();
            yield return new LoseTheHeadgearYouAreWearing();
            yield return new LoseTwoCards();
            yield return new LoseYourClass();
            yield return new LoseYourRace();
            yield return new TrulyObmoxiousCurse();
            yield return new MalignMirror();
        }
    }
}

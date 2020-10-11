using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class Bigfoot : MonsterCard
    {
        public Bigfoot() : base("Bigfoot", 12, 1, 3, 0, false)
        {
        }

        public override Task Play(Table gameContext)
        {
            var currentHero = gameContext.Players.Current;
            var currentHeroIsDwarf = currentHero.Equipped.OfType<DwarfRace>().Any();
            var currentHeroIsHalfling = currentHero.Equipped.OfType<HalflingRace>().Any();

            //var helpingHero = gameContext.Dungeon.HelpingPlayer;
            //var helpingHeroIsDwarf = helpingHero?.Equipped.OfType<DwarfRace>().Any();
            //var helpingHeroIsHalfling = helpingHero?.Equipped.OfType<HalflingRace>().Any();

            //if (currentHeroIsDwarf
            //    || currentHeroIsHalfling
            //    || helpingHeroIsDwarf != null && helpingHeroIsDwarf.Value
            //    || helpingHeroIsHalfling != null && helpingHeroIsHalfling.Value)
            //{
            //    gameContext.Dungeon.State.PlayersStrength += 3;
            //}

            return base.Play(gameContext);
        }

        public override Task BadStuff(Table gameContext)
        {
            gameContext.Players.Current.Equipped
                .OfType<PermanentItemCard>()
                .Where(x => x.WearingType == EWearingType.Headgear)
                .ForEach(x => x.Discard(gameContext));

            return Task.CompletedTask;
        }
    }
}
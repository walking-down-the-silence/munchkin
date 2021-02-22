using System.Linq;
using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class FaceSucker : MonsterCard
    {
        public FaceSucker() : base("Face Sucker", 8, 1, 2, 0, false)
        {
        }

        public override Task Play(Table gameContext)
        {
            var currentHero = gameContext.Players.Current;
            var currentHeroIsElf = currentHero.Equipped.OfType<ElfRace>().Any();

            //var helpingHero = gameContext.Dungeon.HelpingPlayer;
            //var helpingHeroIsElf = helpingHero?.Equipped.OfType<ElfRace>().Any();

            //if (currentHeroIsElf || helpingHeroIsElf != null && helpingHeroIsElf.Value)
            //{
            //    gameContext.Dungeon.PlayersStrength += 6;
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
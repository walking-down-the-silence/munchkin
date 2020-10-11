using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class Amazon : MonsterCard
    {
        public Amazon() : base("Amazon", 8, 1, 2, 0, false)
        {
        }

        public override Task Play(Table state)
        {
            bool isFemale = state.Players.Current.Gender == EGender.Female
                            && !state.Players.Current.Equipped.OfType<ChangeSex>().Any()
                            || state.Players.Current.Gender == EGender.Male
                            && state.Players.Current.Equipped.OfType<ChangeSex>().Any();
            if (isFemale)
            {
                //gameContext.Dungeon.RewardTreasures += 1;
            }
            else
            {
                base.Play(state);
            }

            BoundCards.ForEach(card => card.Play(state));

            return Task.CompletedTask;
        }

        public override Task BadStuff(Table state)
        {
            bool hasClasses = state.Players.Current.Equipped.OfType<ClassCard>().Any();

            if (hasClasses)
            {
                var classes = state.Players.Current.Equipped.Where(x => x is ClassCard || x is SuperMunchkin);

                foreach (var classCard in classes)
                {
                    state.Players.Current.Discard(classCard);
                }
            }
            else
            {
                state.Players.Current.LevelDown();
                state.Players.Current.LevelDown();
                state.Players.Current.LevelDown();
            }

            return Task.CompletedTask;
        }
    }
}
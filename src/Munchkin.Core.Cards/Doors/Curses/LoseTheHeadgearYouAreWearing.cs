using System.Linq;
using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class LoseTheHeadgearYouAreWearing : CurseCard
    {
        public LoseTheHeadgearYouAreWearing() : base("Lose The Headgear You Are Wearing")
        {
        }

        public override Task BadStuff(Table context)
        {
            context.Players.Current.Equipped
                .OfType<PermanentItemCard>()
                .FirstOrDefault(x => x.WearingType == EWearingType.Headgear)
                ?.Discard(context);
            return Task.CompletedTask;
        }
    }
}
using System.Linq;
using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class LoseTheFootgearYouAreWearing : CurseCard
    {
        public LoseTheFootgearYouAreWearing() : base("Lose The Footgear You Are Wearing")
        {
        }

        public override Task Play(Table context)
        {
            context.Players.Current.Equipped
                .OfType<PermanentItemCard>()
                .FirstOrDefault(x => x.WearingType == EWearingType.Footgear)
                ?.Discard(context);
            return Task.CompletedTask;
        }
    }
}
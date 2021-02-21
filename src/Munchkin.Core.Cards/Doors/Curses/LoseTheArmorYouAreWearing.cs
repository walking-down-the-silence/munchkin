using System.Linq;
using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class LoseTheArmorYouAreWearing : CurseCard
    {
        public LoseTheArmorYouAreWearing() : base("Lose The Armor You Are Wearing")
        {
        }

        public override Task BadStuff(Table context)
        {
            context.Players.Current.Equipped
                .OfType<PermanentItemCard>()
                .FirstOrDefault(x => x.WearingType == EWearingType.Armor)
                ?.Discard(context);
            return Task.CompletedTask;
        }
    }
}
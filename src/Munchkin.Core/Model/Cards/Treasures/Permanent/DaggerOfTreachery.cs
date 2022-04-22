using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class DaggerOfTreachery : PermanentItemCard
    {
        public DaggerOfTreachery() : base("Dagger Of Treachery", 3, 0, EItemSize.Small, EWearingType.OneHanded, 400)
        {
            AddProperty(new TheifOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;
using Munchkin.Engine.Original.CardProperties;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class StaffOfNapalm : PermanentItemCard
    {
        public StaffOfNapalm() : base("Staff Of Napalm", 5, 0, EItemSize.Small, EWearingType.OneHanded, 800)
        {
            AddProperty(new WizardOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}
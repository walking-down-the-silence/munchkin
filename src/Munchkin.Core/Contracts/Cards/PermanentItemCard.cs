using Munchkin.Core.Model.Enums;
using Munchkin.Core.Model.Properties;

namespace Munchkin.Core.Contracts.Cards
{
    public abstract class PermanentItemCard : ItemCard
    {
        protected PermanentItemCard(string title, int strength, int runAwayBonus, EItemSize itemSize, EWearingType wearingType, int goldPieces)
            : base(title, strength, runAwayBonus, goldPieces, itemSize)
        {
            AddProperty(new WearingTypeAttribute(wearingType));
        }

        public EWearingType WearingType => GetProperty<WearingTypeAttribute>().WearingType;
    }
}